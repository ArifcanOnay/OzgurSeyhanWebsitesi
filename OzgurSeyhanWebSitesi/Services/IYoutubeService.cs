using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IYouTubeService
    {
        Task<List<Video>> GetPlaylistVideosAsync(string playlistId);
        Task<bool> SyncPlaylistToDatabase(Guid playlistEntityId, string youtubePlaylistId);
        Task<(string title, string thumbnailUrl)> GetPlaylistInfoAsync(string playlistId);
        Task<bool> UpdateAllPlaylistInfoAsync();
    }
}
