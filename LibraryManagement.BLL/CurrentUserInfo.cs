using DTO_LibraryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_LibraryManagement
{
    public static class CurrentUser
    {
        private static UserDTO _CurrentUser ;
        private enum Role {Admin=1,User=2,None=0 }
        private static Role _Role = Role.None;
        public readonly struct UserInfo
        {
            public int UserID { get; }
            public int PersonID { get; }
            public string Username { get; }
            public int RoleID { get; }
            
            public UserInfo(int userID, int personID, string username,int roleID)
            {
                UserID = userID;
                PersonID = personID;
                Username = username;
                RoleID = roleID;
            }
            public static UserInfo Empty => default;

        }

        public static bool IsAdmin()
        {
            if (GetCurrentUserInfo().RoleID ==1)
            {
                return true;
            }
            return false;
        }

        public static UserInfo GetCurrentUserInfo()
        {
            var user = _CurrentUser;
            if (user != null)
                return new UserInfo(user.UserID, user.PersonID, user.Username,user.RoleID);
            return UserInfo.Empty;
        }

        public static void SetUser(UserDTO user)
        {

            _CurrentUser = user;
        }

        public static void Logout()
        {
              _CurrentUser = null;
        }
    }
}
