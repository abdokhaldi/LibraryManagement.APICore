using LibraryManagement.DTO.BorrowingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IBorrowingService
    {
        Task<int> CreateBorrowingAsync(BorrowingForCreationDTO borrowingDTO);

        Task<BorrowingForDisplayDTO?> GetBorrowingDetailsAsync(int id);

        Task<(bool Success, string Error)> ReturnBookAsync(int id);

    }
}
