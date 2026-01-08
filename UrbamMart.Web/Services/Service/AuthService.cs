using UrbamMart.Web.Models;
using UrbamMart.Web.Models.AuthDTO;
using UrbamMart.Web.Services.IService;
using UrbanMart.Web.Models.AuthDTO;
using static UrbamMart.Web.Utilities.StaticDetails;

namespace UrbamMart.Web.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        private readonly string BaseUrl = AuthAPIBase + "/api/auth";

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<bool> AssignRoleAsync(string email, string roleName)
        {
            return await _baseService.SendAsync<bool>(new Models.RequestDto()
            {
                ApiType = ApiType.POST,
                Url = BaseUrl + "/assignRole" + $"?email={email}&roleName={roleName}"
            });
        }

        public async Task<LoginresponseDto?> LoginAsync(LoginRequestDto loginRequest)
        {
            return await _baseService.SendAsync<LoginresponseDto>(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = loginRequest,
                Url = BaseUrl + "/LoginUser"
            }, withBearer: false);
        }

        public async Task<string?> RegisterAsync(RegisterRequestDto registerRequest)
        {
            return await _baseService.SendAsync<string>(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = registerRequest,
                Url = BaseUrl + "/RegisterNewUser"
            }, withBearer: false);
        }
    }
}
