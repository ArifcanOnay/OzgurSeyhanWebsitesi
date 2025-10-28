using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Models
{
   public class Ogretmen:BaseEntitiy
    {
        public string Isim { get; set; }
        public string SoyAd { get; set; }
        public int Yas { get; set; }
        public int DeneyimYili { get; set; }

        public string Bio { get; set; }
        public string Email { get; set; }
        public string YoutubeKanaliUrl { get; set; }
        // Navigation property: bir öğretmenin birden fazla öğrencisi olabilir
        public UserRol Rol { get; set; } = UserRol.Ogretmen;
        public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>();
        public ICollection<Video> Videolar { get; set; } = new List<Video>();
        public ICollection<Spotify> SpotifyLinkleri { get; set; } = new List<Spotify>();


    }
}
