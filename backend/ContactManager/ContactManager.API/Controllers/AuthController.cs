using AutoMapper;
using ContactManager.API.Dtos.Requests;
using ContactManager.Application.Dtos.Responses;
using ContactManager.Application.Services;
using ContactManager.Domain.Dtos;
using ContactManager.Domain.Result;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ContactManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
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
            var loginDto = mapper.Map<AuthLoginDto>(request);
            var result = await service.Login(loginDto);

            var formattedResult = result.Map(token => new { Token = token });
            return HandleResult(formattedResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
        {
            var registerDto = mapper.Map<AuthRegisterDto>(request);
            var result = await service.Register(registerDto); 

            if (result.IsFailure)
            {
                return StatusCode((int)GetStatusCode(result.Status), new { Message = result.Error });
            }

            var userDto = result.Value;
            return CreatedAtAction(actionName: "GetUserById", controllerName: "Users", new { id = userDto.Id }, userDto);
        }

        private HttpStatusCode GetStatusCode(OperationStatus status)
        {
            return status switch
            {
                OperationStatus.NotFound => HttpStatusCode.NotFound,
                OperationStatus.Conflict => HttpStatusCode.Conflict,
                OperationStatus.InvalidData => HttpStatusCode.BadRequest,
                OperationStatus.ServerError => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };
        }

    }
}
