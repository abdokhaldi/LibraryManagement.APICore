
using LibraryManagement.DTO.Common;
using LibraryManagement.DTO.OperationResults;
namespace LibraryManagement.DTO.OperationResult
{
    public class OperationResult : IOperationResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public OperationStatus Status { get; }

        protected OperationResult(bool isSuccess,OperationStatus status,string message)
        {
            IsSuccess = isSuccess;
            Message = message;
            Status = status;
        }

        public static OperationResult Success(string message="") 
            => new (true,OperationStatus.Success,message);
        public static OperationResult Failure(OperationStatus status,string message)
            => new (false,status,message);

    }
    public class OperationResult<T> : OperationResult
    {
        
        public T? Data { get; }
        
        private OperationResult(bool isSuccess,T? data, OperationStatus status, string message)
            : base(isSuccess,status,message)
        {
            
            Data = data;
        }

        public static OperationResult<T> Success(T data,string message = "")
            => new OperationResult<T>(true,data, OperationStatus.Success, message);
        public static new OperationResult<T> Failure(OperationStatus status, string message)
            => new OperationResult<T>(false,default, status, message);

    }

}
