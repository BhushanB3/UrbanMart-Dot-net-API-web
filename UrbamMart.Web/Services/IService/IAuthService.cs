using UrbamMart.Web.Models.AuthDTO;
using UrbanMart.Web.Models.AuthDTO;

namespace UrbamMart.Web.Services.IService
{
    public interface IAuthService
    {
        Task<LoginresponseDto?> LoginAsync(LoginRequestDto loginRequest);
        Task<string?> RegisterAsync(RegisterRequestDto registerRequest);
        Task<bool> AssignRoleAsync(string email, string roleName);
    }
}
