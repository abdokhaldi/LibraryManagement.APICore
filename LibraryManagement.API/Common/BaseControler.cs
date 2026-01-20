using Microsoft.AspNetCore.Mvc;
using LibraryManagement.DTO.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using LibraryManagement.DTO.OperationResults;
namespace LibraryManagement.API.Common
{
    [Authorize]   
    
    [ApiController]
   
    public abstract class BaseController : ControllerBase
    {
       
        protected string CurrentUserID => User.FindFirstValue(ClaimTypes.NameIdentifier)
           ?? throw new InvalidOperationException("User ID not found in claims. Ensure [Authorize] is used.");
       
        protected bool IsAdmin => User.IsInRole("Admin");
        protected string CurrentUserRole => User.FindFirstValue(ClaimTypes.Role)!;
        protected IActionResult HandleErrorResult<T>(T result) where T : IOperationResult
        {
            return result.Status switch
            {
                OperationStatus.NotFound => NotFound(result.Message),
                OperationStatus.Conflict => Conflict(result.Message),
                OperationStatus.Blocked => Conflict(result.Message),
                OperationStatus.Forbidden => Forbid(result.Message),
                _ => BadRequest("An unexpected error occurred .")
            };
        }

    }
}
