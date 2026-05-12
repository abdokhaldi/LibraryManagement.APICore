using LibraryManagement.Domain.TenantContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class Member : IMustHaveTenant
    {
        public int MemberID { get; set; }
        public Guid PersonID { get; set; }
        public Guid TenantID { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Fine> Fines { get; set; } = new List<Fine>();
        public Person Person { get; set; } = null!;
    }
}