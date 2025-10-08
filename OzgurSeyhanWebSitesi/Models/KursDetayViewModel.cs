namespace OzgurSeyhanWebSitesi.Models
{
    public class KursDetayViewModel
    {
        public Kurs Kurs { get; set; } = null!;
        public Ogretmen Ogretmen { get; set; } = null!;
        public string OrnekVideoPath { get; set; } = string.Empty;
        public bool VideoVarMi { get; set; } = false;
    }
}