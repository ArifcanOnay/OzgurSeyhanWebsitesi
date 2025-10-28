using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Dtos
{
    public class CourseDto
    {
        public string KursAdi { get; set; }
        public string Aciklama { get; set; }
        public int OgrenciSayisi { get; set; }
        public string Seviye { get; set; }
        public string IslencekKonular { get; set; }
        public int HaftadaKacGun { get; set; }
        public string HaftaninHangiGunleri { get; set; }
        public string DersSaati { get; set; }
        public string KursunSonundaEldeEdilecekYetenekler { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi
        {
            get; set;
        }
    }
}
