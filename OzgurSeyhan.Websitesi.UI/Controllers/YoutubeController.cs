using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.PlaylistDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos;
using System.Text;
using System.Text.Json;

namespace OzgurSeyhan.Websitesi.UI.Controllers
{
    public class YoutubeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "https://localhost:7101/api";

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
        public async Task<IActionResult> PlaylistCreate(string playlistUrl)
        {
            var client = _httpClientFactory.CreateClient();
            
            var requestBody = new
            {
                playlistUrl = playlistUrl,
                ogretmenId = 1  // Sabit öğretmen ID
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
            return View();
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
        public async Task<IActionResult> VideoCreate(string videoUrl)
        {
            var client = _httpClientFactory.CreateClient();

            var requestBody = new
            {
                youtubeUrl = videoUrl,
                ogretmenId = 1  // Sabit öğretmen ID
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBaseUrl}/YoutubeVideo/from-url", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Video başarıyla eklendi!";
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
    }
}
