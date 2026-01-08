namespace UrbanMart.Services.AuthAPI.Models.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Firstname { get; set; }
        public string? LastName { get; set; }
        public bool IsDeprecated { get; set; }
    }
}
