using UrbanMart.Services.AuthAPI.Models;

namespace UrbanMart.Services.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string TokenGenerator(ApplicationUser user);
    }
}
