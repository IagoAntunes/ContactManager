using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;

namespace ContactManager.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<Result<UserDto>> Register(AuthRegisterDto registerDto)
        {
            var existingUser = await authRepository.GetEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new UserEntity
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = passwordHash
            };
            await authRepository.Register(newUser);
            var userDto = new UserDto
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
            };
            return Result.Success(userDto);
        }
    }
}
