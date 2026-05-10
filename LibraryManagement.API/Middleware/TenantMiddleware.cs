
using LibraryManagement.Shared.Tenant.TenantContract;

namespace LibraryManagement.API.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next) {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, ITenantSetter tenant)
        {
            var tenantIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "tenantID")?.Value;
          
            if (!string.IsNullOrEmpty(tenantIdClaim) && Guid.TryParse(tenantIdClaim, out var tenantId))
            {
                tenant.SetTenantId(tenantId);
            }

           await _next(context);
        }

    }
}
