using System.ComponentModel.DataAnnotations;

namespace UrbanMart.Services.AuthAPI.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
