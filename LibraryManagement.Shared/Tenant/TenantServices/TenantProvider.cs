

using LibraryManagement.Shared.Tenant.TenantContract;

namespace LibraryManagement.Shared.Tenant.TenantService
{
    public class TenantProvider : ITenantGetter, ITenantSetter
    {
        private  Guid _TenantID = Guid.Empty;
        public void SetTenantId(Guid tenantId)
        {
            if (_TenantID != Guid.Empty)
            {
                throw new InvalidOperationException("Tenant context has already been set for this request and cannot be modified.");
            }

            _TenantID = tenantId;
        }
        public Guid GetTenantId()
        {
           return _TenantID ;
        }
    }
}
