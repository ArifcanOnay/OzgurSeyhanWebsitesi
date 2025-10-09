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
    }
}
