using Microsoft.AspNetCore.Mvc;
using OzgurSeyhan.Websitesi.UI.Models;
using System.Diagnostics;

namespace OzgurSeyhan.Websitesi.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "https://localhost:7101/api";

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            // İstatistikleri çek
            var videoCount = 0;
            var playlistCount = 0;
            var ozelDersCount = 0;
            var podcastCount = 0;

            try
            {
                // YouTube Videoları
                var videoResponse = await client.GetAsync($"{_apiBaseUrl}/YoutubeVideo");
                if (videoResponse.IsSuccessStatusCode)
                {
                    var videos = await videoResponse.Content.ReadFromJsonAsync<List<object>>();
                    videoCount = videos?.Count ?? 0;
                }

                // Playlist'ler
                var playlistResponse = await client.GetAsync($"{_apiBaseUrl}/Playlist");
                if (playlistResponse.IsSuccessStatusCode)
                {
                    var playlists = await playlistResponse.Content.ReadFromJsonAsync<List<object>>();
                    playlistCount = playlists?.Count ?? 0;
                }

                // Özel Dersler
                var ozelDersResponse = await client.GetAsync($"{_apiBaseUrl}/OzelDers");
                if (ozelDersResponse.IsSuccessStatusCode)
                {
                    var ozelDersler = await ozelDersResponse.Content.ReadFromJsonAsync<List<object>>();
                    ozelDersCount = ozelDersler?.Count ?? 0;
                }

                // Podcast'ler
                var podcastResponse = await client.GetAsync($"{_apiBaseUrl}/Podcast");
                if (podcastResponse.IsSuccessStatusCode)
                {
                    var podcasts = await podcastResponse.Content.ReadFromJsonAsync<List<object>>();
                    podcastCount = podcasts?.Count ?? 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dashboard istatistikleri yüklenirken hata oluştu");
            }

            ViewBag.VideoCount = videoCount;
            ViewBag.PlaylistCount = playlistCount;
            ViewBag.OzelDersCount = ozelDersCount;
            ViewBag.PodcastCount = podcastCount;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
