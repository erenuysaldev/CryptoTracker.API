using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CryptoTracker.API.Data;
using CryptoTracker.API.Models;
using System.Threading.Tasks;

namespace CryptoTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Kullanıcı kaydı (Register)
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Kullanıcı başarıyla kaydedildi" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(ModelState);
        }

        // Kullanıcı girişi (Login)
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return Ok(new { Message = "Giriş başarılı" });
                    }
                }
                return Unauthorized(new { Message = "Geçersiz giriş" });
            }

            return BadRequest(ModelState);
        }
    }
}
