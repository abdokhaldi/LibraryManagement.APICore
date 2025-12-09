using LibraryManagement.BLL.Interfaces;
using LibraryManagement.DAL;
using LibraryManagement.DAL.Interfaces;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
     }

    }

