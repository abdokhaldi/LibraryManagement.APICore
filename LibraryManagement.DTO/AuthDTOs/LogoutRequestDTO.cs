
using System.ComponentModel.DataAnnotations;
namespace LibraryManagement.DTO.AuthDTOs
{
    public class LogoutRequestDTO
    {
        [Required(ErrorMessage = "Refresh token is required.")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
