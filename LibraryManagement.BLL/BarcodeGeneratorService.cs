using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL
{
    public class BarcodeGeneratorService
    {
        public string GenerateShortBarcode(string prefix = "BC")
        {
            
            string uniqueHash = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

            return $"{prefix}-{uniqueHash}";
           
        }
    }
}
