using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.UserDtos;
using OzgurSeyhanWebSitesi.Core.Services;

namespace OzgurSeyhanWebSitesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(registerDto);
                return Ok(new { success = true, data = user, message = "Kayıt başarılı!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var user = await _userService.LoginAsync(loginDto);
                
                // Session'a kullanıcı bilgisini kaydet
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", $"{user.Ad} {user.Soyad}");

                return Ok(new { success = true, data = user, message = "Giriş başarılı!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { success = true, message = "Çıkış yapıldı!" });
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
            {
                return Unauthorized(new { success = false, message = "Lütfen giriş yapın!" });
            }

            var user = _userService.GetByIdAsync(userId.Value);
            return Ok(new { success = true, data = user });
        }

        [HttpGet("check-session")]
        public IActionResult CheckSession()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
            {
                return Ok(new { success = false, message = "Kullanıcı girişi yok" });
            }

            // Kullanıcı bilgilerini veritabanından al
            var user = _userService.GetByIdAsync(userId.Value);
            
            if (user == null)
            {
                HttpContext.Session.Clear();
                return Ok(new { success = false, message = "Kullanıcı bulunamadı" });
            }

            return Ok(new
            {
                success = true,
                data = user
            });
        }
    }
}
