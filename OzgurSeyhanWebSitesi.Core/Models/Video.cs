using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Models
{
    public class Video : BaseEntitiy
    {
        public string Baslik { get; set; }
        public string Acıklama { get; set; }
        public string YoutubeUrl { get; set; }
        // FK & Navigation
        public Guid OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; }

    }
}
