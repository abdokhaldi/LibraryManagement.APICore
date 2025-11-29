using LibraryManagement.DAL;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LibraryManagement.BLL
{
    

    public static class SecurityService
    {
        // 🛑 يجب تغيير هذا الملح ليكون سرياً ومجهولاً بالنسبة للعامة
        private const string FixedSalt = "YourAppSecureFixedSalt2024!";

        public static string HashPassword(string password)
        {
            string saltedPassword = password + FixedSalt;

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                var sb = new StringBuilder();
                // تحويل البايتات إلى تنسيق سداسي عشري (Hexadecimal)
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // تجزئة كلمة السر المدخلة ومقارنتها بالهاش المخزن
            string enteredHash = HashPassword(enteredPassword);
            return enteredHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
