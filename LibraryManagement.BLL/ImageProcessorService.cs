using System;                   // لـ Guid و Exception
using System.Drawing;           // المكتبة الجديدة
using System.Drawing.Imaging;   // للتعامل مع تنسيقات الصور (JPEG)
using System.IO;                // للتعامل مع MemoryStream والملفات

public static class ImageProcessorService
{
    private const int TargetSize = 150;
    private static string _ImageStorageRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookImages");

    public static string SaveImageToFileSystem(byte[] originalImageBytes)
    {
        if (originalImageBytes == null || originalImageBytes.Length == 0)
        {
            return string.Empty;
        }

        if (!Directory.Exists(_ImageStorageRoot))
        {
            Directory.CreateDirectory(_ImageStorageRoot);
        }

        string uniqueFileName = $"{Guid.NewGuid().ToString()}.jpg";
        string fullPath = Path.Combine(_ImageStorageRoot, uniqueFileName);

        try
        {
            // 1. القراءة من البايت باستخدام MemoryStream (أسلوب System.Drawing)
            using (var ms = new MemoryStream(originalImageBytes))
            using (var originalImage = Image.FromStream(ms))
            {
                // 2. حساب الأبعاد الجديدة للحفاظ على النسبة
                int newWidth = TargetSize;
                int newHeight = (int)(originalImage.Height * TargetSize / originalImage.Width);

                // إذا كان الارتفاع كبيراً، نعيد ضبط العرض (لضمان ألا يتجاوز 150x150)
                if (newHeight > TargetSize)
                {
                    newWidth = (int)(originalImage.Width * TargetSize / originalImage.Height);
                    newHeight = TargetSize;
                }

                // 3. إنشاء نسخة مصغرة باستخدام Graphics
                using (var thumbnail = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(thumbnail))
                {
                    // جودة رسم عالية
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    // رسم الصورة الأصلية على الصورة المصغرة بالأبعاد الجديدة
                    graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);

                    // 4. حفظ الصورة المصغرة على القرص بصيغة JPEG
                    thumbnail.Save(fullPath, ImageFormat.Jpeg);
                }
            }

            return uniqueFileName;
        }
        catch (Exception)
        {
            // يلتقط أي خطأ (صورة تالفة، مشاكل في الصلاحيات، إلخ)
            return string.Empty;
        }
    }

    // ... الكود السابق لـ _ImageStorageRoot ...
    // في كلاس ImageProcessorService (BLL)
    public static void DeleteImageFile(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return; // لا يوجد ملف لحذفه
        }

        // 1. بناء المسار الكامل للملف القديم
        string fullPath = Path.Combine(_ImageStorageRoot, fileName);

        // 2. الحذف الآمن للملف
        try
        {
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        catch (Exception ex)
        {
            // 💡 المفهوم: يجب تسجيل هذا الخطأ في ملف Log. 
            // قد يفشل الحذف إذا كان الملف مفتوحاً بواسطة برنامج آخر، لكن لا يجب أن يعطل التطبيق.
             Console.WriteLine($"Error deleting file {fullPath}: {ex.Message}");
        }
    }
    public static string GetFullImagePath(string fileName)
    {
        // 1. التحقق من أن اسم الملف موجود فعلاً
        if (string.IsNullOrEmpty(fileName))
        {
            return string.Empty; // أو إرجاع مسار صورة افتراضية (No Image Available)
        }

        // 2. بناء المسار الكامل (ROOT + FileName)
        string fullPath = Path.Combine(_ImageStorageRoot, fileName);

        // 3. التحقق من وجود الملف على القرص
        if (File.Exists(fullPath))
        {
            return fullPath;
        }

        // إرجاع مسار فارغ أو مسار "الصورة غير موجودة" إذا لم يتم العثور عليها
        return string.Empty;
    }



}