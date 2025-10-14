using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Services;

namespace OzgurSeyhanWebSitesi.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly IYouTubeService _youTubeService;

        public PlaylistController(IPlaylistService playlistService, IYouTubeService youTubeService)
        {
            _playlistService = playlistService;
            _youTubeService = youTubeService;
        }

        // GET: /Playlist/Index
        public async Task<IActionResult> Index()
        {
            var playlists = await _playlistService.GetActivePlaylistsAsync();
            return View(playlists);
        }

        // GET: /Playlist/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var playlist = await _playlistService.GetPlaylistWithVideosAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: /Playlist/SyncFromYouTube/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SyncFromYouTube(Guid id)
        {
            var playlist = await _playlistService.GetPlaylistByIdAsync(id);

            if (playlist == null || string.IsNullOrEmpty(playlist.YouTubePlaylistId))
            {
                TempData["Error"] = "Playlist bulunamadı veya YouTube Playlist ID eksik!";
                return RedirectToAction(nameof(Index));
            }

            var result = await _youTubeService.SyncPlaylistToDatabase(id, playlist.YouTubePlaylistId);

            if (result)
            {
                TempData["Success"] = $"{playlist.PlaylistAdi} için videolar başarıyla YouTube'dan çekildi!";
            }
            else
            {
                TempData["Error"] = "Senkronizasyon sırasında bir hata oluştu!";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /Playlist/VideoPlayer/5
        public async Task<IActionResult> VideoPlayer(Guid id)
        {
            var video = await _playlistService.GetVideoByIdAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: /Playlist/SyncAllPlaylists - Tüm playlist'leri senkronize et
        [HttpPost]
        public async Task<IActionResult> SyncAllPlaylists()
        {
            try
            {
                // Önce playlist bilgilerini güncelle (başlık ve thumbnail)
                await _youTubeService.UpdateAllPlaylistInfoAsync();
                
                // Sonra videoları senkronize et
                var playlists = await _playlistService.GetActivePlaylistsAsync();
                int successCount = 0;
                int totalCount = playlists.Count();

                foreach (var playlist in playlists)
                {
                    if (!string.IsNullOrEmpty(playlist.YouTubePlaylistId))
                    {
                        var result = await _youTubeService.SyncPlaylistToDatabase(playlist.Id, playlist.YouTubePlaylistId);
                        if (result)
                        {
                            successCount++;
                        }
                    }
                }

                TempData["Success"] = $"Playlist bilgileri güncellendi ve {successCount}/{totalCount} playlist başarıyla senkronize edildi!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Senkronizasyon sırasında bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
