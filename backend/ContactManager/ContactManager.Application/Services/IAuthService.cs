using ContactManager.Domain.Dtos;
using ContactManager.Domain.Result;

namespace ContactManager.Application.Services
{
    public interface IAuthService
    {
        Task<Result<UserDto>> Register(AuthRegisterDto registerDto);
        Task<Result<string>> Login(AuthLoginDto loginDto);
        Task<Result<UserDto>> GetUserData(string userId);
    }
}
