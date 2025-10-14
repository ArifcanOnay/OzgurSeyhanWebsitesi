using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public class VideoService : IVideoService
    {
        private readonly ApplicationDbContext _context;

        public VideoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // OrnekVideo metodları (mevcut sistem)
        public async Task<List<OrnekVideo>> GetAllVideosAsync()
        {
            return await _context.OrnekVideolar
                .Include(v => v.Ogretmen)
                .OrderBy(v => v.SiraNo)
                .ToListAsync();
        }

        public async Task<List<OrnekVideo>> GetVideosByOgretmenIdAsync(Guid ogretmenId)
        {
            return await _context.OrnekVideolar
                .Include(v => v.Ogretmen)
                .Where(v => v.OgretmenId == ogretmenId)
                .OrderBy(v => v.SiraNo)
                .ToListAsync();
        }

        public async Task<OrnekVideo?> GetVideoByIdAsync(Guid id)
        {
            return await _context.OrnekVideolar
                .Include(v => v.Ogretmen)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<bool> CreateVideoAsync(OrnekVideo video)
        {
            try
            {
                _context.OrnekVideolar.Add(video);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateVideoAsync(OrnekVideo video)
        {
            try
            {
                _context.OrnekVideolar.Update(video);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteVideoAsync(Guid id)
        {
            try
            {
                var video = await _context.OrnekVideolar.FindAsync(id);
                if (video != null)
                {
                    _context.OrnekVideolar.Remove(video);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Video metodları (yeni YouTube sistemi)
        public async Task<List<Video>> GetAllYouTubeVideosAsync()
        {
            return await _context.Videos
                .Include(v => v.Playlist)
                .ThenInclude(p => p.Ogretmen)
                .OrderBy(v => v.SiraNo)
                .ToListAsync();
        }

        public async Task<List<Video>> GetVideosByPlaylistIdAsync(Guid playlistId)
        {
            return await _context.Videos
                .Include(v => v.Playlist)
                .Where(v => v.PlaylistId == playlistId)
                .OrderBy(v => v.SiraNo)
                .ToListAsync();
        }

        public async Task<Video?> GetYouTubeVideoByIdAsync(Guid id)
        {
            return await _context.Videos
                .Include(v => v.Playlist)
                .ThenInclude(p => p.Ogretmen)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<bool> CreateYouTubeVideoAsync(Video video)
        {
            try
            {
                _context.Videos.Add(video);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateYouTubeVideoAsync(Video video)
        {
            try
            {
                _context.Videos.Update(video);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteYouTubeVideoAsync(Guid id)
        {
            try
            {
                var video = await _context.Videos.FindAsync(id);
                if (video != null)
                {
                    _context.Videos.Remove(video);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
