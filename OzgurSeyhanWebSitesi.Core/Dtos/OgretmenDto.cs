using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Dtos
{
  public class OgretmenDto:BaseDto
    {
        public string Isim { get; set; }
        public string SoyAd { get; set; }
        public int Yas { get; set; }
        public int DeneyimYili { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string YoutubeKanaliUrl { get; set; }

        // Liste halinde sadece DTO ile video ve spotify bilgisi göstereceğiz
        public List<VideoDto> Videolar { get; set; } = new List<VideoDto>();
        public List<SpotifyDto> SpotifyLinkleri { get; set; } = new List<SpotifyDto>();
        public List<CourseDto> Kurslar { get; set; } = new List<CourseDto>();

    }
}
