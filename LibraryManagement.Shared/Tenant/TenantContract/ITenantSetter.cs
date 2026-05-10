using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Shared.Tenant.TenantContract
{
    public interface ITenantSetter
    {
        void SetTenantId(Guid tenantId);
    }
}
