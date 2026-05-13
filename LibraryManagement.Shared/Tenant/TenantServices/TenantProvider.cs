

using LibraryManagement.Shared.Tenant.TenantContract;

namespace LibraryManagement.Shared.Tenant.TenantService
{
    public class TenantProvider : ITenantGetter, ITenantSetter
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
