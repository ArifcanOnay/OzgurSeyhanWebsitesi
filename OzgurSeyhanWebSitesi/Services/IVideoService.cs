using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IVideoService
    {
        // OrnekVideo metodları (mevcut sistem)
        Task<List<OrnekVideo>> GetAllVideosAsync();
        Task<List<OrnekVideo>> GetVideosByOgretmenIdAsync(Guid ogretmenId);
        Task<OrnekVideo?> GetVideoByIdAsync(Guid id);
        Task<bool> CreateVideoAsync(OrnekVideo video);
        Task<bool> UpdateVideoAsync(OrnekVideo video);
        Task<bool> DeleteVideoAsync(Guid id);

        // Video metodları (yeni YouTube sistemi)
        Task<List<Video>> GetAllYouTubeVideosAsync();
        Task<List<Video>> GetVideosByPlaylistIdAsync(Guid playlistId);
        Task<Video?> GetYouTubeVideoByIdAsync(Guid id);
        Task<bool> CreateYouTubeVideoAsync(Video video);
        Task<bool> UpdateYouTubeVideoAsync(Video video);
        Task<bool> DeleteYouTubeVideoAsync(Guid id);
    }
}
