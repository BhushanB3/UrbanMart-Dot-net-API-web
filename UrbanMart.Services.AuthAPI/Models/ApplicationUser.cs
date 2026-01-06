using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UrbanMart.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string Firstname { get; set; }
        [Required]
        public required string Lastname { get; set; }
        public bool IsDeprecated { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
