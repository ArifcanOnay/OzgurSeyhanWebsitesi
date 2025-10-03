using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}