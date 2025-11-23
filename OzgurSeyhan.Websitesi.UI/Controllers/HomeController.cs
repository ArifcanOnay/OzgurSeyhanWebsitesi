using Microsoft.AspNetCore.Mvc;
using OzgurSeyhan.Websitesi.UI.Models;
using OzgurSeyhanWebSitesi.Core.Dtos.PlaylistDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.OzelDersDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos;
using OzgurSeyhanWebSitesi.Core.Dtos.PodcastDtos;
using System.Diagnostics;

namespace OzgurSeyhan.Websitesi.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5246/api";

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var playlists = new List<PlaylistWithVideosDto>();
            var ozelDersler = new List<OzelDersDto>();
            var ingilizceKonusmaTuyolari = new List<YoutubeVideoDto>();
            var gercekHayattanIngilizce = new List<YoutubeVideoDto>();
            var podcasts = new List<PodcastDto>();

            try
            {
                // Tüm playlists'leri al
                var playlistResponse = await client.GetAsync($"{_apiBaseUrl}/Playlist");
                if (playlistResponse.IsSuccessStatusCode)
                {
                    var allPlaylists = await playlistResponse.Content.ReadFromJsonAsync<List<PlaylistDto>>();
                    
                    if (allPlaylists != null && allPlaylists.Any())
                    {
                        // Her playlist için videoları çek
                        foreach (var playlist in allPlaylists)
                        {
                            try
                            {
                                var withVideosResponse = await client.GetAsync($"{_apiBaseUrl}/Playlist/{playlist.Id}/with-videos");
                                if (withVideosResponse.IsSuccessStatusCode)
                                {
                                    var playlistWithVideos = await withVideosResponse.Content.ReadFromJsonAsync<PlaylistWithVideosDto>();
                                    if (playlistWithVideos != null)
                                    {
                                        playlists.Add(playlistWithVideos);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex, $"Playlist {playlist.Id} videoları yüklenirken hata");
                            }
                        }
                    }
                }

                // İngilizce Konuşma Tüyolarını al (Kategori 2)
                var tuyolarResponse = await client.GetAsync($"{_apiBaseUrl}/YoutubeVideo/by-kategori/2");
                if (tuyolarResponse.IsSuccessStatusCode)
                {
                    var tuyolar = await tuyolarResponse.Content.ReadFromJsonAsync<List<YoutubeVideoDto>>();
                    if (tuyolar != null)
                    {
                        ingilizceKonusmaTuyolari = tuyolar;
                    }
                }

                // Gerçek Hayattan İngilizce videolarını al (Kategori 3)
                var gercekHayatResponse = await client.GetAsync($"{_apiBaseUrl}/YoutubeVideo/by-kategori/3");
                if (gercekHayatResponse.IsSuccessStatusCode)
                {
                    var gercekHayat = await gercekHayatResponse.Content.ReadFromJsonAsync<List<YoutubeVideoDto>>();
                    if (gercekHayat != null)
                    {
                        gercekHayattanIngilizce = gercekHayat;
                    }
                }

                // Özel Dersleri al
                var ozelDersResponse = await client.GetAsync($"{_apiBaseUrl}/OzelDers");
                if (ozelDersResponse.IsSuccessStatusCode)
                {
                    var ozelDersResult = await ozelDersResponse.Content.ReadFromJsonAsync<List<OzelDersDto>>();
                    if (ozelDersResult != null)
                    {
                        ozelDersler = ozelDersResult;
                    }
                }

                // Podcast'leri al
                var podcastResponse = await client.GetAsync($"{_apiBaseUrl}/Podcast");
                if (podcastResponse.IsSuccessStatusCode)
                {
                    var podcastResult = await podcastResponse.Content.ReadFromJsonAsync<List<PodcastDto>>();
                    if (podcastResult != null)
                    {
                        podcasts = podcastResult;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Data yüklenirken hata oluştu");
            }

            ViewBag.OzelDersler = ozelDersler;
            ViewBag.IngilizceKonusmaTuyolari = ingilizceKonusmaTuyolari;
            ViewBag.GercekHayattanIngilizce = gercekHayattanIngilizce;
            ViewBag.Podcasts = podcasts;
            return View(playlists);
        }

        public async Task<IActionResult> Dashboard()
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

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            // Session temizleme API'ye yapılacak
            return RedirectToAction("Index");
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
