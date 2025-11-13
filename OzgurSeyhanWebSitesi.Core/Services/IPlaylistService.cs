using OzgurSeyhanWebSitesi.Core.Dtos.PlaylistDtos;
using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Services
{
    public interface IPlaylistService : IGenericService<Playlist>
    {
        /// <summary>
        /// YouTube Playlist URL'inden playlist oluşturur (sadece playlist kaydı, videolar YouTube'dan çekilir)
        /// </summary>
        Task<PlaylistDto> CreateFromYouTubePlaylistAsync(string playlistUrl, int ogretmenId);

        /// <summary>
        /// Playlist'i videoları ile birlikte getirir (videolar YouTube'dan çekilir)
        /// </summary>
        Task<PlaylistWithVideosDto> GetPlaylistWithVideosAsync(int playlistId);

        /// <summary>
        /// Öğretmene ait tüm playlistleri getirir
        /// </summary>
        Task<IEnumerable<PlaylistDto>> GetByOgretmenIdAsync(int ogretmenId);
    }
}
