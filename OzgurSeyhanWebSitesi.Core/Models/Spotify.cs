using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Models
{
   public class Spotify:BaseEntitiy
    {
        public string Başlık{ get; set; }
        public string SpotifyUrl { get; set; }
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;

        // FK & Navigation
        public Guid OgretmenId { get; set; }
        public Ogretmen Ogretmen{ get; set; }
    }
}
