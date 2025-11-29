using LibraryManagement.DAL;
using LibraryManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL
{
    public class RoleService
    {
        public int RoleID { get; private set; }
        public string RoleName { get; set; }

        public RoleService(int roleID, string roleName)
        {
            RoleID = roleID;
            RoleName = roleName;
        }

        public RoleService()
        {
            RoleID = 0;
            RoleName = "";
        }

        public static RoleService GetRoleByID(int ID)
        {
            var role = RoleRepository.GetRoleByID(ID);

            if (role == null)
            {
                return null;
            }
            return new RoleService(role.RoleID, role.RoleName);
        }

        public static List<RoleService> GetAllRoles()
        {
            var rolesDTO = RoleRepository.GetAllRoles();
            if (rolesDTO == null) return null;

            var roles = rolesDTO.Select(p => new RoleService(p.RoleID,p.RoleName)).ToList();
            return roles;
        }

            
    }
}
