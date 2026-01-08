using System.ComponentModel.DataAnnotations;

namespace UrbamMart.Web.Models.AuthDTO
{
    public class LoginRequestDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
