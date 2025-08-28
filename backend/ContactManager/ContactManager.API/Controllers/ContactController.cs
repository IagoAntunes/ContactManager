using ContactManager.Application.Dtos.Requests;
using ContactManager.Application.Services;
using ContactManager.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Sprache;
using System.Net;
using System.Security.Claims;

namespace ContactManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateContact([FromForm] ContactCreateRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await contactService.CreateAsync(request,userId);

            if (!result.IsSuccess)
            {
                return StatusCode((int)GetStatusCode(result.Status), result.Error);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromForm] ContactUpdateRequest request, [FromRoute] string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await contactService.UpdateAsync(request, id, userId);
            if (!result.IsSuccess)
            {
                return StatusCode((int)GetStatusCode(result.Status), result.Error);
            }
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact([FromRoute] string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await contactService.DeleteAsync(id, userId);

            return result switch
            {
                DeleteResultStatus.Success => NoContent(),
                DeleteResultStatus.NotFound => NotFound(), 
                DeleteResultStatus.Failure => StatusCode(500, "Ocorreu um erro ao tentar deletar o contato."),
                _ => BadRequest()
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            var result = await contactService.GetAll(userId);

            return result.IsSuccess
                ? Ok(result.Value)
                : StatusCode((int)GetStatusCode(result.Status), result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await contactService.GetById(id,userId);

            return result.IsSuccess
                ? Ok(result.Value)
                : StatusCode((int)GetStatusCode(result.Status), result.Error);
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
