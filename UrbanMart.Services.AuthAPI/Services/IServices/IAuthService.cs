using UrbanMart.Services.AuthAPI.Models.DTO;

namespace UrbanMart.Services.AuthAPI.Services.IServices
{
    public interface IAuthService
    {
        Task<LoginresponseDto> Login(LoginRequestDto LoginDto);
        Task<string> Register(RegisterRequestDto registerDto);
        Task<bool> AssignRole(string email, string  roleName);

    }
}
