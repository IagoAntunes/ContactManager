using ContactManager.Application.Services;
using ContactManager.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ContactManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService authService;

        public UsersController(
            IAuthService authService)
        {
            this.authService = authService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await authService.GetUserData(userId!);
            if (result.IsFailure)
            {
                return StatusCode((int)GetStatusCode(result.Status), new { Message = result.Error });
            }

            return Ok(result.Value);
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
