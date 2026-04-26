using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
   
    public class Person
    {
       
        public int PersonID { get; set; }
       
        public string FirstName { get; set; } = null!;
       
        public string LastName { get; set; } = null!;
        public string NationalNumber { get; set; } = null!;
        public string Phone { get; set; }= null!;
       
        public string Email { get; set; } = null!;
        
        public string Address { get; set; } = null!;
       
        public string City { get; set; } = null!;
        
        public char Gender { get; set; }
       
        public bool IsActive { get; set; }
        
    }
}
