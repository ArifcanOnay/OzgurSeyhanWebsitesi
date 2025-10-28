using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Dtos
{
   public  class SpotifyDto:BaseDto
    {
        public string Baslik { get; set; }
        public string SpotifyUrl { get; set; }
        public DateTime EklenmeTarihi { get; set; }
    }
}
