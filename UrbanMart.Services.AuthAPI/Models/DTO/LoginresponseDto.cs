namespace UrbanMart.Services.AuthAPI.Models.DTO
{
    public class LoginresponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
        public string? message { get; set; }
    }
}
