using AutoMapper;
using ContactManager.API.Dtos.Requests;
using ContactManager.Application.Dtos.Responses;
using ContactManager.Application.Services;
using ContactManager.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;
        private readonly IMapper mapper;

        public AuthController(
            IAuthService service,
            IMapper mapper
        )
        {
            this.service = service;
            this.mapper = mapper;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequest request)
        {
            try
            {
                var loginDto = mapper.Map<AuthLoginDto>(request);
                var token = await service.Login(loginDto);
                return Ok(new LoginResponse { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred.");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
        {
            try
            {
                var registerDto = mapper.Map<AuthRegisterDto>(request);
                var user = await service.Register(registerDto);
                return CreatedAtAction(nameof(Register), new { id = user.Value.Id }, new { user.Value.Id, user.Value.Name, user.Value.Email });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Logar o erro aqui
                return StatusCode(500, "An internal error occurred.");
            }
        }

    }
}
