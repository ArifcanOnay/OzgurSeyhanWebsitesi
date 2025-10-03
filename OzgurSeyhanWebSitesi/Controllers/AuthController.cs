using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;
using OzgurSeyhanWebSitesi.Services;
using BCrypt.Net;

namespace OzgurSeyhanWebSitesi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(ApplicationDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            // Kullanıcı var mı kontrol et
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Bu email adresi zaten kullanılıyor.");
            }

            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest("Bu kullanıcı adı zaten kullanılıyor.");
            }

            // Şifreyi hashle
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Yeni kullanıcı oluştur
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Kullanıcı başarıyla oluşturuldu." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Kullanıcıyı bul
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Email veya şifre hatalı.");
            }

            // JWT token oluştur
            var token = _jwtService.GenerateToken(user);

            return Ok(new { token = token });
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}