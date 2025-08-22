using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using MongoDB.Driver;

namespace ContactManager.Infrastructure.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<UserEntity> _usersCollection;
        public AuthRepository(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<UserEntity>("Users");
        }

        public async Task<UserEntity?> GetEmailAsync(string email)
        {
            return await _usersCollection.Find(u => u.Email == email)
                                         .FirstOrDefaultAsync();
        }

        public async Task<Result<bool>> Register(UserEntity user)
        {
            await _usersCollection.InsertOneAsync(user);
            return Result.Success(true);
        }
    }
}
