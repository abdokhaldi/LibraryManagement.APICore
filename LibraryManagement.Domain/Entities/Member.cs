using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class Member
    {
        public int MemberID { get; set; }
        public int PersonID { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Fine> Fines { get; set; } = new List<Fine>();
        public Person Person { get; set; } = null!;
    }
}