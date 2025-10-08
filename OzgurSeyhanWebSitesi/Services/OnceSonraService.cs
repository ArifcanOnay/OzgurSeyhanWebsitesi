using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public class OnceSonraService:IOnceSonraService
    {
        private readonly ApplicationDbContext _context;

        public OnceSonraService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OnceSonra>> GetTumOnceSonralarAsync()
        {
            return await _context.OnceSonralar
                .Include(os => os.Kurs)
                .OrderBy(os => os.OlusturmaTarihi)
                .ToListAsync();
        }

        public async Task<OnceSonra?> GetOnceSonraByIdAsync(Guid id)
        {
            return await _context.OnceSonralar
                .Include(os => os.Kurs)
                .FirstOrDefaultAsync(os => os.Id == id);
        }

        public async Task<IEnumerable<OnceSonra>> GetKursaGoreOnceSonralarAsync(Guid kursId)
        {
            return await _context.OnceSonralar
                .Include(os => os.Kurs)
                .Where(os => os.KursId == kursId)
                .OrderBy(os => os.OlusturmaTarihi)
                .ToListAsync();
        }

        public async Task<IEnumerable<OnceSonra>> GetAktifOnceSonralarAsync()
        {
            // Sample data oluştur (videos klasöründeki videolarla)
            var sampleOnceSonralar = new List<OnceSonra>
            {
                new OnceSonra
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    OgrenciAdi = "Ayşe Yılmaz",
                    OnceVideoUrl = "/videos/OnceSonraBasarıOykusu/OnceVideosu.mp4",
                    SonraVideoUrl = "/videos/OnceSonraBasarıOykusu/SonraVideosu.mp4",
                    OnceAciklama = "İngilizce konuşmaya çekiniyordum. Temel kelimeler bile zordu.",
                    SonraAciklama = "Artık rahatça konuşabiliyorum! İş görüşmelerimde İngilizce konuşuyorum.",
                    AlinanKurslar = "Başlangıç Kursu",
                    KursId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    OlusturmaTarihi = DateTime.Now.AddMonths(-2)
                },
                new OnceSonra
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    OgrenciAdi = "Mehmet Demir",
                    OnceVideoUrl = "/videos/OnceSonraBasarıOykusu/OnceVideosu.mp4",
                    SonraVideoUrl = "/videos/OnceSonraBasarıOykusu/SonraVideosu.mp4",
                    OnceAciklama = "Basit cümleler kurabiliyordum ama akıcı değildim.",
                    SonraAciklama = "Şimdi İngilizce sunumlar yapıyorum. Yurt dışı projelerinde çalışıyorum!",
                    AlinanKurslar = "Orta Seviye Kursu",
                    KursId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    OlusturmaTarihi = DateTime.Now.AddMonths(-1)
                }
            };

            return await Task.FromResult(sampleOnceSonralar);
        }
    }
}
