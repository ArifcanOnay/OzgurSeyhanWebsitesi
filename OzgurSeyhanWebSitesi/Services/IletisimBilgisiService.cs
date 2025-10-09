using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public class IletisimBilgisiService:IIletisimBilgisiService
    {

        private readonly ApplicationDbContext _context;

        public IletisimBilgisiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IletisimBilgisi?> GetAktifIletisimBilgisiAsync()
        {
            return await _context.IletisimBilgileri
                .Include(i => i.Ogretmen)
                .FirstOrDefaultAsync(i => i.Aktif);
        }

        public async Task<IletisimBilgisi?> GetIletisimBilgisiByIdAsync(Guid id)
        {
            return await _context.IletisimBilgileri
                .Include(i => i.Ogretmen)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IletisimBilgisi?> GetIletisimBilgisiByOgretmenIdAsync(Guid ogretmenId)
        {
            return await _context.IletisimBilgileri
                .Include(i => i.Ogretmen)
                .FirstOrDefaultAsync(i => i.OgretmenId == ogretmenId);
        }

        public async Task<IEnumerable<IletisimBilgisi>> GetTumIletisimBilgileriAsync()
        {
            return await _context.IletisimBilgileri
                .Include(i => i.Ogretmen)
                .OrderByDescending(i => i.GuncellemeTarihi)
                .ToListAsync();
        }

        public async Task<IletisimBilgisi> CreateIletisimBilgisiAsync(IletisimBilgisi iletisimBilgisi)
        {
            iletisimBilgisi.Id = Guid.NewGuid();
            iletisimBilgisi.GuncellemeTarihi = DateTime.Now;

            _context.IletisimBilgileri.Add(iletisimBilgisi);
            await _context.SaveChangesAsync();
            return iletisimBilgisi;
        }

        public async Task<IletisimBilgisi?> UpdateIletisimBilgisiAsync(Guid id, IletisimBilgisi iletisimBilgisi)
        {
            var mevcutIletisim = await _context.IletisimBilgileri.FindAsync(id);
            if (mevcutIletisim == null)
                return null;

            mevcutIletisim.TelefonNumarasi = iletisimBilgisi.TelefonNumarasi;
            mevcutIletisim.Email = iletisimBilgisi.Email;
            mevcutIletisim.YouTubeKanali = iletisimBilgisi.YouTubeKanali;
            mevcutIletisim.WhatsAppNumarasi = iletisimBilgisi.WhatsAppNumarasi;
            mevcutIletisim.Adres = iletisimBilgisi.Adres;
            mevcutIletisim.WebSitesi = iletisimBilgisi.WebSitesi;
            mevcutIletisim.Aktif = iletisimBilgisi.Aktif;
            mevcutIletisim.GuncellemeTarihi = DateTime.Now;

            await _context.SaveChangesAsync();
            return mevcutIletisim;
        }

        public async Task<bool> DeleteIletisimBilgisiAsync(Guid id)
        {
            var iletisimBilgisi = await _context.IletisimBilgileri.FindAsync(id);
            if (iletisimBilgisi == null)
                return false;

            _context.IletisimBilgileri.Remove(iletisimBilgisi);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetAktifDurumAsync(Guid id, bool aktif)
        {
            var iletisimBilgisi = await _context.IletisimBilgileri.FindAsync(id);
            if (iletisimBilgisi == null)
                return false;

            iletisimBilgisi.Aktif = aktif;
            iletisimBilgisi.GuncellemeTarihi = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
