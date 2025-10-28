using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Dtos
{
   public class VideoDto:BaseDto
    {
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string YoutubeUrl { get; set; }
    }
}
