using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IPlaylistService
    {
        // Playlist CRUD
        Task<List<Playlist>> GetAllPlaylistsAsync();
        Task<List<Playlist>> GetActivePlaylistsAsync();
        Task<Playlist?> GetPlaylistByIdAsync(Guid id);
        Task<Playlist?> GetPlaylistWithVideosAsync(Guid id);
        Task<bool> CreatePlaylistAsync(Playlist playlist);
        Task<bool> UpdatePlaylistAsync(Playlist playlist);
        Task<bool> DeletePlaylistAsync(Guid id);

        // Video CRUD
        Task<Video?> GetVideoByIdAsync(Guid id);
        Task<bool> CreateVideoAsync(Video video);
        Task<bool> UpdateVideoAsync(Video video);
        Task<bool> DeleteVideoAsync(Guid id);

        // Özel sorgular
        Task<List<Playlist>> GetPlaylistsByOgretmenIdAsync(Guid ogretmenId);
        Task<int> GetVideoCountByPlaylistIdAsync(Guid playlistId);
        Task<Video?> GetVideoByYouTubeIdAsync(string youtubeVideoId, Guid playlistId);
    }
}
