using LibraryManagement.Domain.Entities.Tenants;


namespace LibraryManagement.Domain.TenantContract
{
   public interface IMustHaveTenant
    {
        Guid TenantID { get; set; }
        
    }
}
