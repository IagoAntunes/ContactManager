using ContactManager.Application.Services;
using ContactManager.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageService service;
        private readonly IWebHostEnvironment environment;

        public ImageController(
            IImageService service,
            IWebHostEnvironment environment
        )
        {
            this.service = service;
            this.environment = environment;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageAsync(string id)
        {
            var image = await service.GetImageById(id);
            if (image == null)
            {
                return NotFound();
            }
            var fileName = Path.GetFileName(image.StoredPath);
            var safePath = Path.Combine(environment.ContentRootPath, "Images", fileName);
            if (!System.IO.File.Exists(safePath))
            {
                return NotFound("Metadados da imagem encontrados, mas o arquivo físico está ausente.");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(safePath);
            return File(fileBytes, image.ContentType, enableRangeProcessing: true);
        }   


    }
}
