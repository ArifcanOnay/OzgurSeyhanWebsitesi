namespace OzgurSeyhanWebSitesi.Core.Dtos.OzelDersDtos
{
    public class CreateOzelDersDto
    {
        public string KurSeviyesi { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public int HaftalikSaat { get; set; }
        public int MaksimumOgrenciSayisi { get; set; }
        public string Gunler { get; set; } = string.Empty;
        public string SaatAraligi { get; set; } = string.Empty;
        public int OgretmenId { get; set; }
    }
}
