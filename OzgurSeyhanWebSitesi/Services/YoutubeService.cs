using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;
using System.Text.Json;

namespace OzgurSeyhanWebSitesi.Services
{
    public class YouTubeService : IYouTubeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IPlaylistService _playlistService;
        private readonly ApplicationDbContext _context;
      

        public YouTubeService(HttpClient httpClient, IConfiguration configuration, IPlaylistService playlistService, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _apiKey = configuration["YouTube:ApiKey"] ?? "";
            _playlistService = playlistService;
            _context = context;
        }

        public async Task<List<Video>> GetPlaylistVideosAsync(string playlistId)
        {
            var videos = new List<Video>();
            string nextPageToken = "";

            do
            {
                var url = $"https://www.googleapis.com/youtube/v3/playlistItems?" +
                         $"part=snippet,contentDetails&" +
                         $"maxResults=50&" +
                         $"playlistId={playlistId}&" +
                         $"key={_apiKey}&" +
                         $"pageToken={nextPageToken}";

                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    break;

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonDocument.Parse(json);

                var items = data.RootElement.GetProperty("items");

                int siraNo = 0;
                foreach (var item in items.EnumerateArray())
                {
                    var snippet = item.GetProperty("snippet");
                    var contentDetails = item.GetProperty("contentDetails");

                    videos.Add(new Video
                    {
                        Id = Guid.NewGuid(),
                        VideoBaslik = snippet.GetProperty("title").GetString() ?? "",
                        Aciklama = snippet.GetProperty("description").GetString() ?? "",
                        YouTubeVideoId = contentDetails.GetProperty("videoId").GetString() ?? "",
                        ThumbnailUrl = snippet.GetProperty("thumbnails").GetProperty("high").GetProperty("url").GetString() ?? "",
                        SiraNo = siraNo++,
                        Aktif = true
                    });
                }

                nextPageToken = data.RootElement.TryGetProperty("nextPageToken", out var token)
                    ? token.GetString() ?? ""
                    : "";

            } while (!string.IsNullOrEmpty(nextPageToken));

            return videos;
        }
   

        public async Task<bool> SyncPlaylistToDatabase(Guid playlistEntityId, string youtubePlaylistId)
        {
            try
            {
                var videos = await GetPlaylistVideosAsync(youtubePlaylistId);
                 // Videoları YouTubeVideoId'ye göre tekilleştir
        var uniqueVideos = videos
            .GroupBy(v => v.YouTubeVideoId)
            .Select(g => g.First())
            .ToList();

                foreach (var video in uniqueVideos)
                {
                    video.PlaylistId = playlistEntityId;

                    // DUPLICATE KONTROLÜ - Aynı video varsa ekleme
                    var existingVideo = await _playlistService.GetVideoByYouTubeIdAsync(video.YouTubeVideoId, playlistEntityId);

                    if (existingVideo == null)
                    {
                        await _playlistService.CreateVideoAsync(video);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // YouTube Playlist bilgilerini çek (başlık, thumbnail)
        public async Task<(string title, string thumbnailUrl)> GetPlaylistInfoAsync(string playlistId)
        {
            try
            {
                var url = $"https://www.googleapis.com/youtube/v3/playlists?" +
                         $"part=snippet&" +
                         $"id={playlistId}&" +
                         $"key={_apiKey}";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return ("", "");

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonDocument.Parse(json);

                var items = data.RootElement.GetProperty("items");
                if (items.GetArrayLength() == 0)
                    return ("", "");

                var snippet = items[0].GetProperty("snippet");
                var title = snippet.GetProperty("title").GetString() ?? "";
                
                // Thumbnail URL'ini çek (öncelik sırası: maxres > high > medium > default)
                string thumbnailUrl = "";
                var thumbnails = snippet.GetProperty("thumbnails");
                
                if (thumbnails.TryGetProperty("maxresdefault", out var maxres))
                    thumbnailUrl = maxres.GetProperty("url").GetString() ?? "";
                else if (thumbnails.TryGetProperty("high", out var high))
                    thumbnailUrl = high.GetProperty("url").GetString() ?? "";
                else if (thumbnails.TryGetProperty("medium", out var medium))
                    thumbnailUrl = medium.GetProperty("url").GetString() ?? "";
                else if (thumbnails.TryGetProperty("default", out var defaultThumb))
                    thumbnailUrl = defaultThumb.GetProperty("url").GetString() ?? "";

                return (title, thumbnailUrl);
            }
            catch
            {
                return ("", "");
            }
        }

        // Tüm playlist'lerin bilgilerini güncelle
        public async Task<bool> UpdateAllPlaylistInfoAsync()
        {
            try
            {
                var playlists = await _playlistService.GetActivePlaylistsAsync();
                
                foreach (var playlist in playlists)
                {
                    if (!string.IsNullOrEmpty(playlist.YouTubePlaylistId))
                    {
                        var (title, thumbnailUrl) = await GetPlaylistInfoAsync(playlist.YouTubePlaylistId);
                        
                        if (!string.IsNullOrEmpty(title))
                        {
                            // Veritabanında güncelle
                            playlist.PlaylistAdi = title;
                            playlist.ThumbnailUrl = thumbnailUrl;
                            await _playlistService.UpdatePlaylistAsync(playlist);
                        }
                    }
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
