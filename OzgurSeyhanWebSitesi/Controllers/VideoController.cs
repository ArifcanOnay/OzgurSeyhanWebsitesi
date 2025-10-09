using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Services;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;
        private readonly IOgretmenService _ogretmenService;
        public VideoController(IVideoService videoService, IOgretmenService ogretmenService)
        {
            _videoService = videoService;
            _ogretmenService = ogretmenService;
        }
        // GET: Video/Index - Tüm videoları listele
        public async Task<IActionResult> Index()
        {
            // Önce öğretmen var mı kontrol et
            var ogretmen = await _ogretmenService.GetOgretmenAsync();
            
            // Videolar var mı kontrol et, yoksa ekle
            var videos = await _videoService.GetAllVideosAsync();
            if (!videos.Any() && ogretmen != null)
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
                
                // Tekrar çek
                videos = await _videoService.GetAllVideosAsync();
            }
            
            return View(videos);
        }

        // GET: Video/Details/5 - Video detayını göster  
        public async Task<IActionResult> Details(Guid id)
        {
            var video = await _videoService.GetVideoByIdAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

     
    }
}
