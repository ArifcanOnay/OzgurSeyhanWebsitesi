using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Models;
using OzgurSeyhanWebSitesi.Services;

namespace OzgurSeyhanWebSitesi.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IOgretmenService _ogretmenService;
        private readonly IVideoService _videoService;
        private readonly IIletisimBilgisiService _iletisimBilgisiService;
        private readonly IKursService _kursService;
        private readonly IPlaylistService _playlistService;
        private readonly IYouTubeService _youTubeService;

        public HomeController(IOgretmenService ogretmenService, IVideoService videoService, IIletisimBilgisiService iletisimBilgisiService, IKursService kursService, IPlaylistService playlistService, IYouTubeService youTubeService)
        {
            _ogretmenService = ogretmenService;
            _videoService = videoService;
            _iletisimBilgisiService = iletisimBilgisiService;
            _kursService = kursService;
            _playlistService = playlistService;
            _youTubeService = youTubeService;
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

            // 8. ADIM: Kursları kontrol et ve gerekirse oluştur
            var kursVarMi = await _kursService.KursVarMiAsync();
            if (!kursVarMi && ogretmen != null)
            {
                await _kursService.CreateSampleKurslarAsync(ogretmen.Id);
            }

            // 9. ADIM: İletişim bilgilerini çek veya oluştur
            var iletisimBilgisi = await _iletisimBilgisiService.GetAktifIletisimBilgisiAsync();
            
            // Eğer iletişim bilgisi yoksa oluştur
            if (iletisimBilgisi == null && ogretmen != null)
            {
                var yeniIletisimBilgisi = new IletisimBilgisi
                {
                    TelefonNumarasi = "05354893494",
                    Email = "ozgurseyhan@gmail.com",
                    YouTubeKanali = "https://www.youtube.com/@5dakikadaingilizce",
                    WhatsAppNumarasi = "",
                    Adres = "",
                    WebSitesi = "",
                    Aktif = true,
                    OgretmenId = ogretmen.Id
                };

                await _iletisimBilgisiService.CreateIletisimBilgisiAsync(yeniIletisimBilgisi);
                iletisimBilgisi = await _iletisimBilgisiService.GetAktifIletisimBilgisiAsync();
            }

            // 9. ADIM: Playlist'leri çek
            var playlists = await _playlistService.GetActivePlaylistsAsync();

            // 9.1. ADIM: Otomatik senkronizasyon - Eğer playlist'lerde video yoksa YouTube'dan çek
            await AutoSyncPlaylistsIfNeeded(playlists);

            // 10. ADIM: ViewModel oluştur ve view'a gönder
            var viewModel = new HomeViewModel
            {
                Ogretmen = ogretmen,
                Videos = videos,
                IletisimBilgisi = iletisimBilgisi,
                Playlists = playlists
            };

            return View(viewModel);
        }

        // Otomatik senkronizasyon method'u - İlk açılışta videoları çeker
        private async Task AutoSyncPlaylistsIfNeeded(IEnumerable<Playlist> playlists)
        {
            try
            {
                var syncTasks = new List<Task>();

                foreach (var playlist in playlists)
                {
                    if (!string.IsNullOrEmpty(playlist.YouTubePlaylistId))
                    {
                        // Video sayısını kontrol et
                        var videoCount = await _playlistService.GetVideoCountByPlaylistIdAsync(playlist.Id);
                        
                        if (videoCount == 0)
                        {
                            // Arkaplanda senkronize et (UI'yi bloklamadan)
                            var syncTask = Task.Run(async () =>
                            {
                                try
                                {
                                    // Önce playlist bilgilerini güncelle (başlık, thumbnail)
                                    var (title, thumbnailUrl) = await _youTubeService.GetPlaylistInfoAsync(playlist.YouTubePlaylistId);
                                    if (!string.IsNullOrEmpty(title))
                                    {
                                        playlist.PlaylistAdi = title;
                                        playlist.ThumbnailUrl = thumbnailUrl;
                                        await _playlistService.UpdatePlaylistAsync(playlist);
                                    }

                                    // Sonra videoları senkronize et
                                    await _youTubeService.SyncPlaylistToDatabase(playlist.Id, playlist.YouTubePlaylistId);
                                }
                                catch
                                {
                                    // Hata durumunda sessizce devam et
                                }
                            });
                            
                            syncTasks.Add(syncTask);
                        }
                    }
                }

                // Tüm senkronizasyon işlemlerini başlat (UI'yi bekletmeden)
                if (syncTasks.Any())
                {
                    _ = Task.WhenAll(syncTasks); // Fire and forget
                }
            }
            catch
            {
                // Hata durumunda sessizce devam et
            }
        }
    }
}