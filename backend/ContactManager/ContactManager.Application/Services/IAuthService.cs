using ContactManager.Domain.Dtos;

namespace ContactManager.Application.Services
{
    public interface IAuthService
    {
        Task<Result<UserDto>> Register(AuthRegisterDto registerDto);
    }
}
