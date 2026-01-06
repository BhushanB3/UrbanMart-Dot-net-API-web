using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrbanMart.Services.AuthAPI.Data;
using UrbanMart.Services.AuthAPI.Models;
using UrbanMart.Services.AuthAPI.Models.DTO;
using UrbanMart.Services.AuthAPI.Services.IServices;

namespace UrbanMart.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(ApplicationDBContext db, UserManager<ApplicationUser> userManager, 
            IJwtTokenGenerator jwtTokenGenerator, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleManager = roleManager;
        }

        public async Task<LoginresponseDto> Login(LoginRequestDto LoginDto)
        {
            var user =  await _db.ApplicationUsers.Where(
                                        c => c.UserName.ToUpper() == LoginDto.UserName.ToUpper()).FirstOrDefaultAsync();

            if(user != null)
            {
                bool isValid = await _userManager.CheckPasswordAsync(user, LoginDto.Password);
                if(isValid)
                {
                    var token = _jwtTokenGenerator.TokenGenerator(user);

                    UserDto userDto = new()
                    {
                        UserName = user.UserName,
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        LastName = user.Lastname,
                        Firstname = user.Firstname,
                        IsDeprecated = user.IsDeprecated
                    };

                    LoginresponseDto result = new()
                    {
                        User = userDto,
                        Token = token,
                        message = "logged in successfully"
                    };

                    return result;
                }
                else
                {
                    return new LoginresponseDto() { User = null, message = "Password not match"};
                }
            }
            else
            {
                return new LoginresponseDto() { User = null, message="User not found"};
            }
        }

        public async Task<string> Register(RegisterRequestDto registerDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Firstname = registerDto.Firstname,
                Lastname = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                NormalizedEmail = registerDto.Email.ToUpper(),
                NormalizedUserName = registerDto.UserName.ToUpper()
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    return string.Empty;
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return "Exception caught, please try later";
            }
        }
        
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.Where(c => c.Email.ToLower() == email.ToLower()).FirstOrDefault();

            if(user != null)
            {
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
    }
}
