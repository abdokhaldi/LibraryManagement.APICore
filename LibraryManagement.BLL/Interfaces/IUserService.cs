using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterUserAsync(UserForCreationDTO userDTO);
        Task<UserForDisplayDTO?> GetUserDerailsAsync(int id);
    }
}
