using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Models;
using OzgurSeyhanWebSitesi.Services;

namespace OzgurSeyhanWebSitesi.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IOgretmenService _ogretmenService;
        private readonly IVideoService _videoService;

        public HomeController(IOgretmenService ogretmenService, IVideoService videoService)
        {
            _ogretmenService = ogretmenService;
            _videoService = videoService;
        }

        // Ana sayfa action'ı - Öğretmen bilgilerini getirecek
        public async Task<IActionResult> Index()
        {
            // 1. ADIM: Veritabanından öğretmen var mı kontrol et
            var ogretmen = await _ogretmenService.GetOgretmenAsync();
            
            // 2. ADIM: Eğer öğretmen yoksa otomatik oluştur
            if (ogretmen == null)
            {
                // Yeni öğretmen modeli oluştur
                var yeniOgretmen = new Ogretmen
                {
                    AdSoyad = "Özgür Seyhan",
                    Ozgecmis = "Cumhuriyetimizin 100. yılında Türkiye'deki herkese İngilizce öğretme fikrini kafasına takmış bir deli. 7'den 77'ye, yerli ve yabancı her seviyeden öğrencilerle 10 bin saati aşkın online İngilizce ders tecrübesine sahip bir öğretmen.",
                    Egitim = "İngilizce Öğretmenliği Lisans Mezunu",
                    DeneyimYili = 10,
                    ProfilFotoUrl = "/images/ozgur-seyhan.jpg",
                    YouTubeKanalUrl = "https://www.youtube.com/@5dakikadaingilizce",
                    OlusturmaTarihi = DateTime.Now
                };

                // 3. ADIM: Veritabanına kaydet
                var olusturuldu = await _ogretmenService.CreateOgretmenAsync(yeniOgretmen);
                
                if (olusturuldu)
                {
                    // 4. ADIM: Kaydedilen öğretmeni tekrar çek
                    ogretmen = await _ogretmenService.GetOgretmenAsync();
                }
                else
                {
                    // Eğer kaydetme başarısız olursa örnek veri gönder
                    ogretmen = yeniOgretmen;
                }
            }

            // 6. ADIM: Videolar var mı kontrol et, yoksa ekle
            var mevcutVideolar = await _videoService.GetAllVideosAsync();
            if (!mevcutVideolar.Any() && ogretmen != null)
            {
                // İlk video
                var video1 = new OrnekVideo
                {
                    Baslik = "İngilizce Canlı Ders Örneği - 1",
                    VideoUrl = "https://www.youtube.com/embed/8mXO_WSijYc",
                    Aciklama = "Hocamızın canlı dersinden bir örnek. İngilizce öğrenmenin eğlenceli yolları.",
                    DersYontemi = "Interactive Learning",
                    SiraNo = 1,
                    OgretmenId = ogretmen.Id,
                    OlusturmaTarihi = DateTime.Now
                };

                // İkinci video
                var video2 = new OrnekVideo
                {
                    Baslik = "İngilizce Canlı Ders Örneği - 2", 
                    VideoUrl = "https://www.youtube.com/embed/alDTmb6kBK8",
                    Aciklama = "Pratik İngilizce konuşma teknikleri ve günlük hayat kelimeleri.",
                    DersYontemi = "Speaking Practice",
                    SiraNo = 2,
                    OgretmenId = ogretmen.Id,
                    OlusturmaTarihi = DateTime.Now
                };

                // Videoları kaydet
                await _videoService.CreateVideoAsync(video1);
                await _videoService.CreateVideoAsync(video2);
            }

            // 7. ADIM: Videoları çek
            var videos = await _videoService.GetAllVideosAsync();

            // 8. ADIM: ViewModel oluştur ve view'a gönder
            var viewModel = new HomeViewModel
            {
                Ogretmen = ogretmen,
                Videos = videos
            };

            return View(viewModel);
        }
    }
}