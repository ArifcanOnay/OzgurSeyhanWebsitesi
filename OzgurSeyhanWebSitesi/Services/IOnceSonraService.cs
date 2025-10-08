using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IOnceSonraService
    {

        Task<IEnumerable<OnceSonra>> GetTumOnceSonralarAsync();
        Task<OnceSonra?> GetOnceSonraByIdAsync(Guid id);
        Task<IEnumerable<OnceSonra>> GetKursaGoreOnceSonralarAsync(Guid kursId);
        Task<IEnumerable<OnceSonra>> GetAktifOnceSonralarAsync();
    }
}
