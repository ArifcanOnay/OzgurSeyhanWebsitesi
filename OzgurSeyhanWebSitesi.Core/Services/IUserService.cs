using OzgurSeyhanWebSitesi.Core.Dtos.UserDtos;

namespace OzgurSeyhanWebSitesi.Core.Services
{
    public interface IUserService : IGenericService<UserDto>
    {
        Task<UserDto> RegisterAsync(UserRegisterDto registerDto);
        Task<UserDto> LoginAsync(UserLoginDto loginDto);
        Task<UserDto> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}
