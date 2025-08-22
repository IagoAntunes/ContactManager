using ContactManager.Domain.Entities;

namespace ContactManager.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
    }
}
