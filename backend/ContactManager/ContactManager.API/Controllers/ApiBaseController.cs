using ContactManager.Domain.Result;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsFailure)
            {
                var errorResponse = new { Message = result.Error };
                return StatusCode((int)GetStatusCode(result.Status), errorResponse);
            }

            return Ok(result.Value);
        }

        private HttpStatusCode GetStatusCode(OperationStatus status)
        {
            return status switch
            {
                OperationStatus.InvalidData => HttpStatusCode.BadRequest,
                OperationStatus.Conflict => HttpStatusCode.Conflict,
                OperationStatus.NotFound => HttpStatusCode.NotFound,
                OperationStatus.ServerError => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}