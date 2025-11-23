using AutoMapper;
using OzgurSeyhanWebSitesi.Core.Dtos.UserDtos;
using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Repositories;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Core.UnitOfWorks;
using System.Security.Cryptography;
using System.Text;

namespace OzgurSeyhanWebSitesi.Bussinies.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<User> _repository;
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IUnitOfWorks unitOfWorks, IMapper mapper, IUserRepository userRepository)
        {
            _repository = repository;
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> RegisterAsync(UserRegisterDto registerDto)
        {
            // Email kontrolü
            if (await _userRepository.EmailExistsAsync(registerDto.Email))
            {
                throw new Exception("Bu email adresi zaten kayıtlı!");
            }

            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = HashPassword(registerDto.Password);
            user.KayitTarihi = DateTime.UtcNow;
            user.AktifMi = true;

            await _userRepository.AddAsync(user);
            await _unitOfWorks.CommitAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            
            if (user == null)
            {
                throw new Exception("Email veya şifre hatalı!");
            }

            if (!VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new Exception("Email veya şifre hatalı!");
            }

            if (!user.AktifMi)
            {
                throw new Exception("Hesabınız aktif değil!");
            }

            // Son giriş tarihini güncelle
            user.SonGirisTarihi = DateTime.UtcNow;
            await _unitOfWorks.CommitAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hash;
        }

        // IGenericService implementations
        public async Task AddAsync(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);
            await _repository.AddAsync(user);
            await _unitOfWorks.CommitAsync();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _unitOfWorks.Commit();
        }

        public List<UserDto> GetAll()
        {
            var users = _repository.GetAll();
            return _mapper.Map<List<UserDto>>(users);
        }

        public UserDto GetByIdAsync(int id)
        {
            var user = _repository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public void Update(UserDto entity)
        {
            var user = _mapper.Map<User>(entity);
            _repository.Update(user);
            _unitOfWorks.Commit();
        }
    }
}
