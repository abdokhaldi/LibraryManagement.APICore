

using LibraryManagement.Shared.Tenant.TenantContract;

namespace LibraryManagement.Shared.Tenant.TenantService
{
    internal class TenantService : ITenantProvider, ITenantSetter
    {
        public Guid TenantID { get; private set; }
        public void SetTenant(Guid tenantId)
        {
            TenantID = tenantId;
        }
    }
}
