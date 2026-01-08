using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UrbanMart.Services.AuthAPI.Models.DTO;
using UrbanMart.Services.AuthAPI.Services;
using UrbanMart.Services.AuthAPI.Services.IServices;

namespace UrbanMart.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<ActionResult> Login(LoginRequestDto model)
        {
            var result = await _authService.Login(model);
            if(result.User == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("RegisterNewUser")]
        public async Task<ActionResult> Register(RegisterRequestDto model)
        {
            var result = await _authService.Register(model);
            if(!string.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }
            return Ok();
        }

        [HttpPost]
        [Route("assignRole")]
        public async Task<ActionResult> AssignRole(string email, string roleName)
        {
            var result = await _authService.AssignRole(email, roleName.ToUpper());
            if (result)
            {
                return Ok();
            }
            return BadRequest(result);
        }
    }
}
