using ContactManager.Domain.Entities;
using ContactManager.Domain.Result;

namespace ContactManager.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<Result<UserEntity>> Register(UserEntity user);
        Task<UserEntity?> GetEmailAsync(string email);
        Task<Result<UserEntity>> GetUserData(string userId);
    }
}
