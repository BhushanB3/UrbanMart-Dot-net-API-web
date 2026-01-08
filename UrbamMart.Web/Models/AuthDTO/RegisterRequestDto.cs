using System.ComponentModel.DataAnnotations;

namespace UrbanMart.Web.Models.AuthDTO
{
    public class RegisterRequestDto
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        [Required]
        public required string Password { get; set; }
        public string RoleName { get; set; }
    }
}
