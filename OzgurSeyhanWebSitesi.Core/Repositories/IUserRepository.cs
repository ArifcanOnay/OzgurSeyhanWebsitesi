using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Repository.Repositories
{
   public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> GetByKullaniciAdiAsync(string kullaniciAdi);
        Task<User> GetByEmailAsync(string email);

    }
}
