using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.PlaylistDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.PodcastDtos;
using OzgurSeyhan.Websitesi.UI.Models;
using System.Text;
using System.Text.Json;

namespace OzgurSeyhan.Websitesi.UI.Controllers
{
    public class YoutubeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5246/api";

        public YoutubeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region Playlist İşlemleri

        // Playlist Listesi
        public async Task<IActionResult> PlaylistIndex()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/Playlist");

            if (response.IsSuccessStatusCode)
            {
                var playlists = await response.Content.ReadFromJsonAsync<List<PlaylistDto>>();
                return View(playlists);
            }

            return View(new List<PlaylistDto>());
        }

        // Playlist Ekleme Sayfası
        [HttpGet]
        public IActionResult PlaylistCreate()
        {
            return View();
        }

        // Playlist Ekleme İşlemi
        [HttpPost]
        public async Task<IActionResult> PlaylistCreate(CreatePlaylistViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();
            
          
            
            var requestBody = new
            {
                playlistUrl = model.PlaylistUrl,
                kategoriBaslik = model.KategoriBaslik,
                ogretmenId = model.OgretmenId
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBaseUrl}/Playlist/from-url", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Playlist başarıyla eklendi!";
                return RedirectToAction("PlaylistIndex");
            }

            var error = await response.Content.ReadAsStringAsync();
            TempData["ErrorMessage"] = $"Hata: {error}";
            return View(model);
        }

        // Playlist Silme
        [HttpPost]
        public async Task<IActionResult> PlaylistDelete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}/Playlist/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Playlist başarıyla silindi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Playlist silinirken hata oluştu!";
            }

            return RedirectToAction("PlaylistIndex");
        }

        // Playlist Videoları Görüntüleme
        public async Task<IActionResult> PlaylistVideos(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/Playlist/{id}/with-videos");

            if (response.IsSuccessStatusCode)
            {
                var playlist = await response.Content.ReadFromJsonAsync<PlaylistWithVideosDto>();
                return View(playlist);
            }

            return RedirectToAction("PlaylistIndex");
        }

        #endregion

        #region Video İşlemleri

        // Video Listesi
        public async Task<IActionResult> VideoIndex()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/YoutubeVideo");

            if (response.IsSuccessStatusCode)
            {
                var videos = await response.Content.ReadFromJsonAsync<List<YoutubeVideoDto>>();
                return View(videos);
            }

            return View(new List<YoutubeVideoDto>());
        }

        // Video Ekleme Sayfası
        [HttpGet]
        public IActionResult VideoCreate()
        {
            return View();
        }

        // Video Ekleme İşlemi
        [HttpPost]
        public async Task<IActionResult> VideoCreate(string videoUrl, string? kategoriBaslik, int kategori = 1)
        {
            var client = _httpClientFactory.CreateClient();

            var requestBody = new
            {
                youtubeUrl = videoUrl,
                ogretmenId = 1,  // Sabit öğretmen ID
                kategori = kategori,  // Kategori: 1=YoutubeVideolarim, 2=IngilizceKonusmaTuyolari
                kategoriBaslik = kategoriBaslik  // Opsiyonel kategori başlığı
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBaseUrl}/YoutubeVideo/from-url", content);

            if (response.IsSuccessStatusCode)
            {
                var kategoriAdi = kategori == 1 ? "YouTube Videolarım" : "İngilizce Konuşma Tüyoları";
                TempData["SuccessMessage"] = $"Video '{kategoriAdi}' bölümüne başarıyla eklendi!";
                return RedirectToAction("VideoIndex");
            }

            var error = await response.Content.ReadAsStringAsync();
            TempData["ErrorMessage"] = $"Hata: {error}";
            return View();
        }

        // Video Güncelleme Sayfası
        [HttpGet]
        public async Task<IActionResult> VideoEdit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/YoutubeVideo/{id}");

            if (response.IsSuccessStatusCode)
            {
                var video = await response.Content.ReadFromJsonAsync<YoutubeVideoDto>();
                return View(video);
            }

            return RedirectToAction("VideoIndex");
        }

        // Video Güncelleme İşlemi
        [HttpPost]
        public async Task<IActionResult> VideoEdit(YoutubeVideoDto video)
        {
            var client = _httpClientFactory.CreateClient();

            var updateDto = new
            {
                id = video.Id,
                baslik = video.Baslik,
                url = video.Url,
                videoId = video.VideoId,
                ogretmenId = video.OgretmenId
            };

            var json = JsonSerializer.Serialize(updateDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_apiBaseUrl}/YoutubeVideo/{video.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Video başarıyla güncellendi!";
                return RedirectToAction("VideoIndex");
            }

            TempData["ErrorMessage"] = "Video güncellenirken hata oluştu!";
            return View(video);
        }

        // Video Silme
        [HttpPost]
        public async Task<IActionResult> VideoDelete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}/YoutubeVideo/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Video başarıyla silindi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Video silinirken hata oluştu!";
            }

            return RedirectToAction("VideoIndex");
        }

        #endregion

        #region Podcast İşlemleri

        // Podcast Listesi
        public async Task<IActionResult> PodcastIndex()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/Podcast");

            if (response.IsSuccessStatusCode)
            {
                var podcasts = await response.Content.ReadFromJsonAsync<List<PodcastDto>>();
                return View(podcasts);
            }

            return View(new List<PodcastDto>());
        }

        // Podcast Ekleme Sayfası
        [HttpGet]
        public IActionResult PodcastCreate()
        {
            return View();
        }

        // Podcast Ekleme İşlemi
        [HttpPost]
        public async Task<IActionResult> PodcastCreate(string baslik, string podcastUrl, IFormFile? kapakResmi)
        {
            if (string.IsNullOrWhiteSpace(baslik) || string.IsNullOrWhiteSpace(podcastUrl))
            {
                TempData["ErrorMessage"] = "Başlık ve Podcast URL boş olamaz!";
                return View();
            }

            var client = _httpClientFactory.CreateClient();
            string? kapakResmiYolu = null;

            // Resim yükleme işlemi
            if (kapakResmi != null && kapakResmi.Length > 0)
            {
                try
                {
                    // Resim dosya adı oluştur
                    var fileName = $"podcast_{Guid.NewGuid()}{Path.GetExtension(kapakResmi.FileName)}";
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "podcasts");
                    
                    // Klasör yoksa oluştur
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var filePath = Path.Combine(uploadsFolder, fileName);

                    // Dosyayı kaydet
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await kapakResmi.CopyToAsync(fileStream);
                    }

                    kapakResmiYolu = $"/images/podcasts/{fileName}";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Resim yüklenirken hata oluştu: {ex.Message}";
                    return View();
                }
            }

            var requestBody = new
            {
                baslik = baslik,
                podcastUrl = podcastUrl,
                kapakResmi = kapakResmiYolu,
                ogretmenId = 1  // Sabit öğretmen ID
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBaseUrl}/Podcast", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Podcast başarıyla eklendi!";
                return RedirectToAction("PodcastIndex");
            }

            var error = await response.Content.ReadAsStringAsync();
            TempData["ErrorMessage"] = $"Hata: {error}";
            return View();
        }

        // Podcast Güncelleme Sayfası
        [HttpGet]
        public async Task<IActionResult> PodcastEdit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/Podcast/{id}");

            if (response.IsSuccessStatusCode)
            {
                var podcast = await response.Content.ReadFromJsonAsync<PodcastDto>();
                return View(podcast);
            }

            return RedirectToAction("PodcastIndex");
        }

        // Podcast Güncelleme İşlemi
        [HttpPost]
        public async Task<IActionResult> PodcastEdit(int id, string baslik, string podcastUrl)
        {
            var client = _httpClientFactory.CreateClient();

            var updateDto = new
            {
                id = id,
                baslik = baslik,
                podcastUrl = podcastUrl,
                ogretmenId = 1
            };

            var json = JsonSerializer.Serialize(updateDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_apiBaseUrl}/Podcast/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Podcast başarıyla güncellendi!";
                return RedirectToAction("PodcastIndex");
            }

            TempData["ErrorMessage"] = "Podcast güncellenirken hata oluştu!";
            return RedirectToAction("PodcastEdit", new { id });
        }

        // Podcast Silme
        [HttpPost]
        public async Task<IActionResult> PodcastDelete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}/Podcast/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Podcast başarıyla silindi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Podcast silinirken hata oluştu!";
            }

            return RedirectToAction("PodcastIndex");
        }

        #endregion
    }
}
