using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IIletisimBilgisiService
    {
          Task<IletisimBilgisi?> GetAktifIletisimBilgisiAsync();
        Task<IletisimBilgisi?> GetIletisimBilgisiByIdAsync(Guid id);
        Task<IletisimBilgisi?> GetIletisimBilgisiByOgretmenIdAsync(Guid ogretmenId);
        Task<IEnumerable<IletisimBilgisi>> GetTumIletisimBilgileriAsync();
        Task<IletisimBilgisi> CreateIletisimBilgisiAsync(IletisimBilgisi iletisimBilgisi);
        Task<IletisimBilgisi?> UpdateIletisimBilgisiAsync(Guid id, IletisimBilgisi iletisimBilgisi);
        Task<bool> DeleteIletisimBilgisiAsync(Guid id);
        Task<bool> SetAktifDurumAsync(Guid id, bool aktif);
    }
}