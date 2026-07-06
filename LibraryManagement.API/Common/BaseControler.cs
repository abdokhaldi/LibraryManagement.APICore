using Microsoft.AspNetCore.Mvc;
using LibraryManagement.DTO.Common;
using System.Security.Claims;
using LibraryManagement.DTO.OperationResults;
namespace LibraryManagement.API.Common
{
   // [Authorize]   
    
    [ApiController]
   
    public abstract class BaseController : ControllerBase
    {
       
         protected string CurrentUserID => User.FindFirstValue(ClaimTypes.NameIdentifier)
     ?? throw new InvalidOperationException("User ID (NameIdentifier) not found in claims. Ensure [Authorize] is used on the endpoint.");
        protected bool IsAdmin => User.IsInRole("Admin");
        protected string CurrentUserRole => User.FindFirstValue(ClaimTypes.Role)??"Member";
        
        protected IActionResult HandleErrorResult<T>(T result) where T : IOperationResult
        {
            return result.Status switch
            {
                OperationStatus.NotFound => NotFound(new { Message = result.Message }),
                OperationStatus.Conflict => Conflict(new { Message = result.Message }),
                OperationStatus.Blocked => Conflict(new { Message = result.Message }),
                OperationStatus.Forbidden => StatusCode(StatusCodes.Status403Forbidden,new { Message = result.Message }),
                _ => BadRequest("An unexpected error occurred .")
            };
        }

    }
}
