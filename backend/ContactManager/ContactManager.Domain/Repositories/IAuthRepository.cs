using ContactManager.Domain.Entities;

namespace ContactManager.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<Result<bool>> Register(UserEntity user);
        Task<UserEntity?> GetEmailAsync(string email);
    }
}
