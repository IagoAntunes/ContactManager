using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;

namespace ContactManager.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly ITokenService tokenService;

        public AuthService(
            IAuthRepository authRepository,
            ITokenService tokenService
            )
        {
            this.authRepository = authRepository;
            this.tokenService = tokenService;
        }

        public async Task<string> Login(AuthLoginDto loginDto)
        {
            var user = await authRepository.GetEmailAsync(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var token = tokenService.GenerateToken(user);
            return token;
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
                PasswordHash = passwordHash,
                Roles = new List<string> { "User" } 
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
