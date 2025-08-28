using AutoMapper;
using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using ContactManager.Domain.Result;

namespace ContactManager.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AuthService(
            IAuthRepository authRepository,
            ITokenService tokenService,
            IMapper mapper
            )
        {
            this.authRepository = authRepository;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        public async Task<Result<UserDto>> GetUserData(string userId)
        {
            var resultGetUserData = await authRepository.GetUserData(userId);
            var finalResult = resultGetUserData.Map(userEntity => mapper.Map<UserDto>(userEntity));

            return finalResult;
        }

        public async Task<Result<string>> Login(AuthLoginDto loginDto)
        {
            var user = await authRepository.GetEmailAsync(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return Result<string>.Failure(OperationStatus.InvalidData, $"Login ou Senha");
            }

            var token = tokenService.GenerateToken(user);
            return Result<string>.Success(token);
        }

        public async Task<Result<UserDto>> Register(AuthRegisterDto registerDto)
        {
            var existingUser = await authRepository.GetEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return Result<UserDto>.Failure(OperationStatus.Conflict, $"Email ja existe");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var newUser = new UserEntity
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Roles = new List<string> { "User" } 
            };
            var resultCreateUser = await authRepository.Register(newUser);
            var finalResult = resultCreateUser.Map(contactEntity => mapper.Map<UserDto>(contactEntity));

            return finalResult;
        }
    }
}
