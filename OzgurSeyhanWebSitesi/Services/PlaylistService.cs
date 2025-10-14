using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly ApplicationDbContext _context;

        public PlaylistService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Playlist CRUD İşlemleri
        public async Task<List<Playlist>> GetAllPlaylistsAsync()
        {
            return await _context.Playlists
                .Include(p => p.Ogretmen)
                .Include(p => p.Videolar)
                .OrderBy(p => p.SiraNo)
                .ToListAsync();
        }

        public async Task<List<Playlist>> GetActivePlaylistsAsync()
        {
            return await _context.Playlists
                .Include(p => p.Videolar)
                .Where(p => p.Aktif)
                .OrderBy(p => p.SiraNo)
                .ToListAsync();
        }

        public async Task<Playlist?> GetPlaylistByIdAsync(Guid id)
        {
            return await _context.Playlists
                .Include(p => p.Ogretmen)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Playlist?> GetPlaylistWithVideosAsync(Guid id)
        {
            return await _context.Playlists
                .Include(p => p.Ogretmen)
                .Include(p => p.Videolar.OrderBy(v => v.SiraNo))
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> CreatePlaylistAsync(Playlist playlist)
        {
            try
            {
                _context.Playlists.Add(playlist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdatePlaylistAsync(Playlist playlist)
        {
            try
            {
                _context.Playlists.Update(playlist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeletePlaylistAsync(Guid id)
        {
            try
            {
                var playlist = await GetPlaylistByIdAsync(id);
                if (playlist == null) return false;

                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Video CRUD İşlemleri
        public async Task<Video?> GetVideoByIdAsync(Guid id)
        {
            return await _context.Videos
                .Include(v => v.Playlist)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<bool> CreateVideoAsync(Video video)
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

        public async Task<bool> UpdateVideoAsync(Video video)
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

        public async Task<bool> DeleteVideoAsync(Guid id)
        {
            try
            {
                var video = await GetVideoByIdAsync(id);
                if (video == null) return false;

                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Özel Sorgular
        public async Task<List<Playlist>> GetPlaylistsByOgretmenIdAsync(Guid ogretmenId)
        {
            return await _context.Playlists
                .Include(p => p.Videolar)
                .Where(p => p.OgretmenId == ogretmenId && p.Aktif)
                .OrderBy(p => p.SiraNo)
                .ToListAsync();
        }

        public async Task<int> GetVideoCountByPlaylistIdAsync(Guid playlistId)
        {
            return await _context.Videos
                .Where(v => v.PlaylistId == playlistId && v.Aktif)
                .CountAsync();
        }
        public async Task<Video?> GetVideoByYouTubeIdAsync(string youtubeVideoId, Guid playlistId)
        {
            return await _context.Videos
                .FirstOrDefaultAsync(v => v.YouTubeVideoId == youtubeVideoId && v.PlaylistId == playlistId);
        }
    }
}
