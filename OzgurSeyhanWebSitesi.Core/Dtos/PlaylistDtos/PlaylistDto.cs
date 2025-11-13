using OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Dtos.PlaylistDtos
{
    public class PlaylistDto : BaseDto
    {
        public string PlaylistId { get; set; } = string.Empty;
        public string Baslik { get; set; } = string.Empty;
        public string? Aciklama { get; set; }
        public int OgretmenId { get; set; }
    }

    public class CreatePlaylistDto
    {
        public string PlaylistUrl { get; set; } = string.Empty;
        public int OgretmenId { get; set; }
    }

    public class PlaylistWithVideosDto : PlaylistDto
    {
        // Videoları YouTube'dan çekerken kullanacağız
        public List<YoutubeVideoDto> Videos { get; set; } = new List<YoutubeVideoDto>();
    }
}
