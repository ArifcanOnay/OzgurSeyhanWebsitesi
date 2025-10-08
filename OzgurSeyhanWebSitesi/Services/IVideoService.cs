using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public interface IVideoService
    {
        Task<List<OrnekVideo>> GetAllVideosAsync();
        Task<List<OrnekVideo>> GetVideosByOgretmenIdAsync(Guid ogretmenId);
        Task<OrnekVideo?> GetVideoByIdAsync(Guid id);
        Task<bool> CreateVideoAsync(OrnekVideo video);
        Task<bool> UpdateVideoAsync(OrnekVideo video);
        Task<bool> DeleteVideoAsync(Guid id);
    }
}
