using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Core.UnitOfWorks;
using OzgurSeyhanWebSitesi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Service.Services
{
    public class UserService:Service<User>,IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUnitOfWorks unitOfWorks, IGenericRepository<User> repository,IUserRepository userRepository) : base(unitOfWorks, repository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
             return await _userRepository.GetByEmailAsync(email);

        }

        public async Task<User> GetByKullaniciAdiAsync(string kullaniciAdi)
        {
             return await _userRepository.GetByKullaniciAdiAsync(kullaniciAdi);

        }
    }
}
