

using LibraryManagement.Shared.Tenant.TenantContract;

namespace LibraryManagement.Shared.Tenant.TenantService
{
    public class TenantProvider : ITenantGetter, ITenantSetter
    {
        private Guid _TenantID { get; set; }
        public void SetTenantId(Guid tenantId)
        {
            _TenantID = tenantId;
        }
        public Guid GetTenantId()
        {
           return _TenantID ;
        }
    }
}
