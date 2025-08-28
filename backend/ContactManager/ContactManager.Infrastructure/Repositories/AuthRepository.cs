using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using ContactManager.Domain.Result;
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

        public async Task<Result<UserEntity>> GetUserData(string userId)
        {
            var user = await _usersCollection.Find(u => u.Id == userId)
                                             .FirstOrDefaultAsync();

            if (user == null)
            {
                return Result<UserEntity>.Failure(OperationStatus.Conflict, $"Usuário não encontrado");
            }
            return Result<UserEntity>.Success(user);
        }

        public async Task<Result<UserEntity>> Register(UserEntity user)
        {
            await _usersCollection.InsertOneAsync(user);
            return Result<UserEntity>.Success(user);
        }
    }
}
