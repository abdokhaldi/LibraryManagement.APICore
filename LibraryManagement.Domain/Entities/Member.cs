

namespace LibraryManagement.Domain.Entities
{
    
    public class Member
    {
        
        public int MemberID { get; set; }
       
        public int PersonID { get; set; }
        
        public Person Person { get; set; } = null!;
      
        public DateTime JoinDate { get; set; }
       
        public bool IsActive { get; set; }
    }
}
