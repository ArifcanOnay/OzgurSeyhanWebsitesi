using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Core.Services
{
    public interface IUserService:IService<User>
    {
        Task<User> GetByKullaniciAdiAsync(string kullaniciAdi);
        Task<User> GetByEmailAsync(string email);
    }
}
