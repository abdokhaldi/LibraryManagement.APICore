

using LibraryManagement.Shared.Tenant.TenantContract;

namespace LibraryManagement.Shared.Tenant.TenantService
{
    public class TenantService : ITenantGetter, ITenantSetter
    {
        public Guid TenantID { get; private set; }
        public void SetTenantId(Guid tenantId)
        {
            TenantID = tenantId;
        }
        public Guid GetTenantId()
        {
           return TenantID ;
        }
    }
}
