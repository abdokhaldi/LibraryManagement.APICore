

using LibraryManagement.Domain.TenantContract;

namespace LibraryManagement.Domain.Entities
{
   
    public class Person : IMustHaveTenant
    {
       
        public Guid PersonID { get; set; }
        public Guid TenantID { get; set; }
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
