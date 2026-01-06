using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.DTO.Common;

namespace LibraryManagement.DTO.OperationResults
{
    public interface IOperationResult
    {
        bool IsSuccess { get; }
        string Message { get; }
        OperationStatus Status { get; } 
    }
}
