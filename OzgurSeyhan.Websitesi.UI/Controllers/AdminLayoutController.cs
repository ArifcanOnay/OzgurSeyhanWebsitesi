using Microsoft.AspNetCore.Mvc;

namespace OzgurSeyhan.Websitesi.UI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }

        // YouTube Video Yönetimi
        public IActionResult YoutubeVideos()
        {
            return RedirectToAction("VideoIndex", "Youtube");
        }

        // Playlist Yönetimi
        public IActionResult Playlists()
        {
            return RedirectToAction("PlaylistIndex", "Youtube");
        }

        // Podcast Yönetimi
        public IActionResult Podcasts()
        {
            return RedirectToAction("PodcastIndex", "Youtube");
        }

        // Özel Ders Yönetimi
        public IActionResult OzelDersler()
        {
            return RedirectToAction("Index", "OzelDers");
        }
    }
}
