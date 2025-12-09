using System;
using System.IO;
using SixLabors.ImageSharp;           // 💡 المكتبة الجديدة
using SixLabors.ImageSharp.Processing; // 💡 لإجراءات التعديل (Resizing)
using SixLabors.ImageSharp.Formats.Jpeg;

namespace LibraryManagement.BLL
{
    public static class ImageProcessorService
    {
        private const int TargetSize = 150;
        // ملاحظة: يُفضل استخدام IWebHostEnvironment أو IHostingEnvironment لتحديد مسار التخزين
        // لكن للحفاظ على الكلاس ثابتاً، سنترك Path.Combine مؤقتاً.
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

            string uniqueFileName = $"{Guid.NewGuid()}.jpg";
            string fullPath = Path.Combine(_ImageStorageRoot, uniqueFileName);

            try
            {
                // 1. استخدام ImageSharp لقراءة وتعديل الصورة
                using (var image = Image.Load(originalImageBytes))
                {
                    // 2. تطبيق التصغير مع الحفاظ على النسبة
                    // هذا يضمن أن الصورة لن تتجاوز 150x150
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(TargetSize, TargetSize),
                        Mode = ResizeMode.Max // يضمن أن أحد الأبعاد لن يتجاوز الهدف، ويحافظ على النسبة
                    }));

                    // 3. حفظ الصورة المصغرة بصيغة JPEG
                    // تحديد الجودة والحفظ مباشرة إلى الملف
                    image.Save(fullPath, new JpegEncoder { Quality = 80 });
                }

                return uniqueFileName;
            }
            catch (Exception)
            {
                // التعامل مع أي خطأ أثناء معالجة الصورة
                return string.Empty;
            }
        }

        // ... دوال DeleteImageFile و GetFullImagePath لا تحتاج تعديل ...
        // ملاحظة: يجب أيضاً تغيير BitMap إلى Bitmap
    }
}