using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Models
{
    public class HomeViewModel
    {
        public Ogretmen? Ogretmen { get; set; }
        public List<OrnekVideo> Videos { get; set; } = new List<OrnekVideo>();
        public IletisimBilgisi? IletisimBilgisi { get; set; }
    }
}