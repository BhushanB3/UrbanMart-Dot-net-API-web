namespace UrbanMart.Web.Models.AuthDTO
{
    public class LoginresponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
        public string? message { get; set; }
    }
}
