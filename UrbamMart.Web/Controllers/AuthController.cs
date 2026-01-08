using Microsoft.AspNetCore.Mvc;
using UrbamMart.Web.Models.AuthDTO;
using UrbamMart.Web.Services.IService;
using UrbanMart.Web.Models.AuthDTO;

namespace UrbamMart.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        //for loin page
        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await authService.LoginAsync(model);
                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    //store the token in session
                    HttpContext.Session.SetString("JWToken", response.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = "Login failed. Please check your credentials.";
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            return View();
        }
    }
}
