using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Dtos
{
    public class UserDto
    {

        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public UserRol Role { get; set; }
    }

    }
