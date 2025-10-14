using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public class KursService : IKursService
    {
        private readonly ApplicationDbContext _context;

        public KursService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kurs>> GetAktifKurslarAsync()
        {
            // Önce veritabanından kursları çek
            var veritabaniKurslari = await _context.Kurslar
                .Include(k => k.Ogretmen)
                .Where(k => k.Aktif)
                .ToListAsync();

            // Eğer veritabanında kurs varsa, onları döndür
            if (veritabaniKurslari.Any())
            {
                return veritabaniKurslari;
            }

            // Eğer veritabanında kurs yoksa, static sample data döndür
            var kurslar = new List<Kurs>
            {
                new Kurs
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    KursAdi = "Beginner English Course",
                    Aciklama = "İngilizce öğrenmeye sıfırdan başlayanlar için tasarlanmış kapsamlı temel kurs programı.",
                    BeklenenSeviye = "A1-A2",
                    KursSuresiHafta = 12,
                    HaftalikDersSayisi = 3,
                    DersGunleri = "Pazartesi, Çarşamba, Cuma",
                    YeniFiyat = 800,
                    EskiFiyat = 1000,
                    IndirimdeMi = true,
                    RenkKodu = "#28a745",
                    Aktif = true
                },
                new Kurs
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    KursAdi = "Intermediate English Course",
                    Aciklama = "Temel İngilizce bilgisi olan öğrenciler için orta seviye konuşma ve yazma becerileri geliştirme kursu.",
                    BeklenenSeviye = "A2-B1",
                    KursSuresiHafta = 14,
                    HaftalikDersSayisi = 3,
                    DersGunleri = "Pazartesi, Cuma, Cumartesi",
                    YeniFiyat = 1000,
                    EskiFiyat = 1200,
                    IndirimdeMi = true,
                    RenkKodu = "#007bff",
                    Aktif = true
                }
            };

            return kurslar;
        }

        public async Task<Kurs?> GetKursByIdAsync(Guid id)
        {
            return await _context.Kurslar
                .Include(k => k.Ogretmen)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<KursDetayViewModel?> GetKursDetayAsync(Guid id)
        {
            // Birinci kurs - Başlangıç Kursu
            if (id == Guid.Parse("00000000-0000-0000-0000-000000000001"))
            {
                // Sample öğretmen oluştur
                var sampleOgretmen = new Ogretmen
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    AdSoyad = "Özgür Seyhan",
                    Ozgecmis = "10 yıllık İngilizce eğitimi deneyimi",
                    Egitim = "İngiliz Dili ve Edebiyatı",
                    ProfilFotoUrl = "/images/ozgur-seyhan.jpg",
                    YouTubeKanalUrl = "https://youtube.com/@ozgurseyhan"
                };

                // Sample kurs oluştur
                var sampleKurs = new Kurs
                {
                    Id = id,
                    KursAdi = "Temel İngilizce Kursu",
                    Aciklama = "A1-A2 seviyesinde kapsamlı İngilizce eğitimi",
                    Konular = "A1-A2 Seviye İngilizce, Dil Egzersizleri, Temel Gramer, Kelime Dağarcığı",
                    BeklenenSeviye = "İngilizce konuşmak için mükemmel bir temel atmış olacaksınız!",
                    KursSeviyesi = 1,
                    KursSuresiHafta = 12,
                    HaftalikDersSayisi = 3,
                    ToplamDersSayisi = 36,
                    DersGunleri = "Pazartesi, Cuma, Cumartesi",
                    DersSaatleri = "19:00-20:00",
                    EskiFiyat = 1000,
                    YeniFiyat = 800,
                    IndirimdeMi = true,
                    RenkKodu = "#28a745",
                    PopulerMi = true,
                    Aktif = true,
                    OgretmenId = sampleOgretmen.Id,
                    Ogretmen = sampleOgretmen
                };

                return new KursDetayViewModel
                {
                    Kurs = sampleKurs,
                    Ogretmen = sampleOgretmen,
                    OrnekVideoPath = "/videos/HİM HİS HER İT MY MİNE ME.mp4",
                    VideoVarMi = true
                };
            }

            // İkinci kurs - Orta Seviye Kursu
            if (id == Guid.Parse("00000000-0000-0000-0000-000000000002"))
            {
                // Sample öğretmen oluştur
                var sampleOgretmen = new Ogretmen
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    AdSoyad = "Özgür Seyhan",
                    Ozgecmis = "10 yıllık İngilizce eğitimi deneyimi",
                    Egitim = "İngiliz Dili ve Edebiyatı",
                    ProfilFotoUrl = "/images/ozgur-seyhan.jpg",
                    YouTubeKanalUrl = "https://youtube.com/@ozgurseyhan"
                };

                // İkinci kurs oluştur
                var sampleKurs = new Kurs
                {
                    Id = id,
                    KursAdi = "Orta Seviye İngilizce Kursu",
                    Aciklama = "A2-B1 seviyesinde gelişmiş İngilizce eğitimi",
                    Konular = "A2-B1 Seviye İngilizce, İleri Konuşma Teknikleri, Akademik Yazma, İş İngilizcesi Temelleri",
                    BeklenenSeviye = "Günlük yaşamda rahatça İngilizce konuşabilir ve yazabilirsiniz!",
                    KursSeviyesi = 2,
                    KursSuresiHafta = 14,
                    HaftalikDersSayisi = 3,
                    ToplamDersSayisi = 42,
                    DersGunleri = "Pazartesi, Cuma, Cumartesi",
                    DersSaatleri = "20:00-21:00",
                    EskiFiyat = 1200,
                    YeniFiyat = 1000,
                    IndirimdeMi = true,
                    RenkKodu = "#007bff",
                    PopulerMi = false,
                    Aktif = true,
                    OgretmenId = sampleOgretmen.Id,
                    Ogretmen = sampleOgretmen
                };

                return new KursDetayViewModel
                {
                    Kurs = sampleKurs,
                    Ogretmen = sampleOgretmen,
                    OrnekVideoPath = "/videos/Tüm modallar tekrar.mp4",
                    VideoVarMi = true
                };
            }

            // Gerçek veritabanı sorgusu
            var kurs = await GetKursByIdAsync(id);

            if (kurs == null)
                return null;

            return new KursDetayViewModel
            {
                Kurs = kurs,
                Ogretmen = kurs.Ogretmen,
                OrnekVideoPath = "/videos/kurs1-ornek-video.mp4",
                VideoVarMi = false
            };
        }

        public async Task<Kurs> CreateKursAsync(Kurs kurs)
        {
            kurs.Id = Guid.NewGuid();
            kurs.OlusturmaTarihi = DateTime.Now;

            _context.Kurslar.Add(kurs);
            await _context.SaveChangesAsync();
            return kurs;
        }

        public async Task<bool> KursVarMiAsync()
        {
            return await _context.Kurslar.AnyAsync();
        }

        public async Task CreateSampleKurslarAsync(Guid ogretmenId)
        {
            // Beginner Course
            var beginnerKurs = new Kurs
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                KursAdi = "Beginner English Course",
                Aciklama = "İngilizce öğrenmeye sıfırdan başlayanlar için tasarlanmış kapsamlı temel kurs programı.",
                Konular = "Temel Gramer, Kelime Dağarcığı, Basit Konuşma",
                BeklenenSeviye = "A1-A2",
                KursSeviyesi = 1,
                KursSuresiHafta = 12,
                HaftalikDersSayisi = 3,
                ToplamDersSayisi = 36,
                DersGunleri = "Pazartesi, Çarşamba, Cuma",
                DersSaatleri = "19:00-20:00",
                YeniFiyat = 800,
                EskiFiyat = 1000,
                IndirimdeMi = true,
                RenkKodu = "#28a745",
                PopulerMi = true,
                Aktif = true,
                OgretmenId = ogretmenId,
                OlusturmaTarihi = DateTime.Now
            };

            // Intermediate Course
            var intermediateKurs = new Kurs
            {
                Id = new Guid("00000000-0000-0000-0000-000000000002"),
                KursAdi = "Intermediate English Course",
                Aciklama = "Temel İngilizce bilgisi olan öğrenciler için orta seviye konuşma ve yazma becerileri geliştirme kursu.",
                Konular = "İleri Gramer, Konuşma Teknikleri, Akademik Yazma",
                BeklenenSeviye = "A2-B1",
                KursSeviyesi = 2,
                KursSuresiHafta = 14,
                HaftalikDersSayisi = 3,
                ToplamDersSayisi = 42,
                DersGunleri = "Pazartesi, Cuma, Cumartesi",
                DersSaatleri = "20:00-21:00",
                YeniFiyat = 1000,
                EskiFiyat = 1200,
                IndirimdeMi = true,
                RenkKodu = "#007bff",
                PopulerMi = false,
                Aktif = true,
                OgretmenId = ogretmenId,
                OlusturmaTarihi = DateTime.Now
            };

            _context.Kurslar.AddRange(beginnerKurs, intermediateKurs);
            await _context.SaveChangesAsync();
        }
    }
}
