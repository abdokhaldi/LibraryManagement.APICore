using DTO_LibraryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_LibraryManagement
{
    public class OperationResultBLL
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object ReturnedValue { get; set; }

        public OperationResultBLL(string message, bool result)
        {
            this.Message = message;
            this.Success = result;
        }
        public OperationResultBLL()
        {
            this.Message = "";
            this.Success = false;
        }

        public static OperationResultBLL Ok(string message = "The Operation was successful.")
        {
            return new OperationResultBLL(message, true);
        }
        public static OperationResultBLL Fail(string message = "The Operation was failed !")
        {
            return new OperationResultBLL(message, false);
        }

    }
}
