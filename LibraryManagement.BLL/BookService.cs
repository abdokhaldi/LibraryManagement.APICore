
using LibraryManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL
{
    public class BookService
    {
       public BookService(IBookRepository book)
        {

        }

       /* public void ImageToSave ()
        {
            // ... منطق التحقق من البيانات الأخرى ...

            // ** 1. استدعاء معالج الصور هنا (القلب النابض للتحسين) **
            if (this.ImageBytesToSave != null && this.ImageBytesToSave.Length > 0)
            {
                // 1. حفظ المسار القديم
                string oldFileName = this.ImagePath;

                // 2. معالجة وحفظ الصورة الجديدة (وتحديث this.ImagePath بالاسم الجديد)
              // نستدعي الدالة التي تنفذ: التصغير -> الحفظ على القرص -> إرجاع اسم الملف
                string savedFileName = ImageProcessorService.SaveImageToFileSystem(this.ImageBytesToSave);

                // نحدث الخاصية التي ستُخزن في قاعدة البيانات
                this.ImagePath = savedFileName;
                
                if (!string.IsNullOrEmpty(oldFileName))
                {
                    // نستدعي دالة جديدة في ImageProcessorService للقيام بالحذف الآمن
                    ImageProcessorService.DeleteImageFile(oldFileName);
                }
            }
            else
            {
                // إذا لم يتم إرسال صورة جديدة، نحافظ على المسار الحالي في وضع التعديل
                // أو نتركه فارغاً إذا كنا في وضع الإضافة
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    this.ImagePath = string.Empty;
                }
            }
 }*/

    }
}
