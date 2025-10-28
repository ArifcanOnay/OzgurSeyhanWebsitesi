using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Models
{
    public class Ogrenci:BaseEntitiy
    {
        public string KullaniciAdi { get; set; }

        public string PasswordHash { get; set; }   // JWT öncesi şifre hashli tutulur
        public string Email { get; set; }
        public UserRol Rol { get; set; } = UserRol.Ogrenci;

    }
}
