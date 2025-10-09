using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IOgretmenService
    {
        Task<Ogretmen?> GetOgretmenAsync();
        Task<bool> CreateOgretmenAsync(Ogretmen ogretmen);
        Task<bool> UpdateOgretmenAsync(Ogretmen ogretmen);
        Task<bool> DeleteOgretmenAsync(Guid id);
    }
}
