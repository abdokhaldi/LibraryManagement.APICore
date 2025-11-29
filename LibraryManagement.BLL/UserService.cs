using LibraryManagement.DAL;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.BLL
{
    public class UserService
    {
        private enum Mode { AddNew=1,Update=2}
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoleID { get; set; }
        public RoleService RoleInfo = null;
        
        private PersonService PersonInfo =null;
        public string FullName { get; set; }
        public string Phone { get;set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

        private Mode _Mode = Mode.AddNew;
        

        private UserService(int userID, int personID,  string username, 
                     string password, int roleID, bool isActive,DateTime createdAt)
        {
            UserID = userID;
            PersonID = personID;
            Username = username;
            Password = password;
            RoleID = roleID;
            IsActive = isActive;
            CreatedAt = createdAt;
            RoleInfo = RoleService.GetRoleByID(RoleID);
            PersonInfo = PersonService.FindPersonByID(PersonID);
            FullName = PersonInfo.FullName;
            Phone = PersonInfo.Phone;
            Email = PersonInfo.Email;
            RoleName = RoleInfo.RoleName;
            _Mode = Mode.Update;

        }
        public UserService()
        {
            UserID = 0;
            PersonID = 0;
            Username = "";
            Password = "";
            RoleID = 0;
            IsActive = true;
            CreatedAt = DateTime.Now;
            _Mode = Mode.AddNew;
            RoleInfo = null;
            PersonInfo = null;
            FullName = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
         }
        private UserService(UserDTO user)
        {
            UserID = user.UserID;
            PersonID = user.PersonID;
            Username = user.Username;
            RoleID = user.RoleID;
            IsActive = user.IsActive;
            CreatedAt = user.CreatedAt;
        }


        public static UserService GetUserByID(int ID)
        {
            var dto = UserRepository.GetUserByID(ID);
            return new UserService(dto.UserID,dto.PersonID,dto.Username,dto.Password,dto.RoleID,dto.IsActive,dto.CreatedAt);
        }
        
        public static List<UserService> GetAllUsers()
        {
            var usersList = UserRepository.GetAllUsers();
            
            if (usersList == null)  return null;

            var users = usersList.Select(
                u => new UserService(u.UserID, u.PersonID, u.Username, u.Password, u.RoleID, u.IsActive,u.CreatedAt)
                ).ToList();

            return users;
        }
        public static bool IsUsernameExists(string username)
        {
            var result = UserRepository.IsUsernameExists(username);
            if (result == null)
                return false;

            return true;
        }
        private UserDTO _FillObjectToTransfer()
        {
            return new UserDTO
            {
                UserID = UserID,
                PersonID = PersonID,
                Username = Username,
                Password = Password,
                RoleID = RoleID,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
            };
        }
        private bool _AddNewUser()
        {
            var userDTO = _FillObjectToTransfer();
            
            /// ADD user  and return  newUserID
            if (userDTO != null) { 
                UserID = UserRepository.AddNewUser(userDTO);
               }

        if(UserID > 0)
            {
                ActivityRepository.AddActivity("Add", $"Add User: {UserID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "User", UserID);
                return true;
            }
            return false;
        }

        private bool _UpdateUser()
        {
            var userDTO = _FillObjectToTransfer();
            
            int rowsAffected = UserRepository.UpdateUser(userDTO);
               if(rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Update", $"Update User: {UserID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "User", UserID);
                return true;
            }
            return false;
        }

        public OperationResultBLL Save()
        {
            if (string.IsNullOrEmpty(Password) || Password.Length < 6)
            {
                return OperationResultBLL.Ok("the password must be at list 6 characters."); ;
            }
            if (!CurrentUser.IsAdmin())
                return OperationResultBLL.Ok("You are not admin , you don't have a permission to edit users !");
            if (_Mode==Mode.AddNew || _Mode == Mode.Update)
            {
                if (!string.IsNullOrEmpty(this.Password))
                {
                    this.Password = SecurityService.HashPassword(Password);
                }
            }
            
          switch (_Mode)
            {
                case Mode.AddNew:

                if (_AddNewUser())
            {
                _Mode = Mode.Update;
                return  OperationResultBLL.Ok("User has been added successfully .");
                    }
                    else
                    {
                        return OperationResultBLL.Fail("User cannot be Added !");
                    }
                    
                case Mode.Update:
                    if (!_UpdateUser())
                        return OperationResultBLL.Fail("User cannot be updated !");
                    return OperationResultBLL.Ok("User updated successfully.");
              }
            return OperationResultBLL.Fail();
          }
       
        public static bool DeactivateUser(int userID)
        {
            int rowsAffected = UserRepository.SetUserActiveStatus(userID,false);
            if (rowsAffected > 0){
                ActivityRepository.AddActivity("Deactivate", $"Deactivate User: {userID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "User", userID);
                return true;
            }
            return false;
        }
        public static bool ActivateUser(int userID)
        {
            int rowsAffected = UserRepository.SetUserActiveStatus(userID, true);
            if (rowsAffected > 0)
            {
                ActivityRepository.AddActivity("Activate", $"Activate User: {userID}", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "User", userID);
                return true;
            }
            return false;
        }

        public static OperationResultBLL Login(string username, string enteredPassword)
        {
            var userDTO = UserRepository.GetUserByUsername(username);
            if (userDTO == null) return OperationResultBLL.Fail("User not exists!");
            if(!userDTO.IsActive)
                return OperationResultBLL.Fail("Your account is currently inactive. Please contact administration.");

            string storedHash = userDTO.Password;
            if (SecurityService.VerifyPassword(enteredPassword,storedHash))
            {
                CurrentUser.SetUser(userDTO);
                ActivityRepository.AddActivity("Login", $"User: {userDTO.Username} has logged in", DateTime.Now, CurrentUser.GetCurrentUserInfo().Username, "User",userDTO.UserID);
               return OperationResultBLL.Ok("Login successful .");
            }
            return OperationResultBLL.Fail("Password not correct !");
        }
      }

    }

