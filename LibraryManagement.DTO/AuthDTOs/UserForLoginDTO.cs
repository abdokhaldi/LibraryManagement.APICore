using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTO.AuthDTOs
{
    public class UserForLoginDTO
    {
          public required string Identifier{get;set;}
           public required string Password { get; set; }
       }
}
