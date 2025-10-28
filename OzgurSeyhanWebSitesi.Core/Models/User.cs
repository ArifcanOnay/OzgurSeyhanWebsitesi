using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Models
{
 public class User:BaseEntitiy
    {
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public byte[] PassworhSalt { get; set; }   // JWT öncesi şifre hashli tutulur
        public byte[] PasswordHash { get; set; }
        public UserRol Role { get; set; }
    }
}
