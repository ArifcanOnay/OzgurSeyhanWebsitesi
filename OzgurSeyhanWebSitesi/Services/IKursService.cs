using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IKursService
    {
        Task<IEnumerable<Kurs>> GetAktifKurslarAsync();
        Task<Kurs?> GetKursByIdAsync(Guid id);
        Task<KursDetayViewModel?> GetKursDetayAsync(Guid id);
        Task<Kurs> CreateKursAsync(Kurs kurs);
        Task<bool> KursVarMiAsync();
        Task CreateSampleKurslarAsync(Guid ogretmenId);
    }
}
