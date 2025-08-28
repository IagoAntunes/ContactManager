using ContactManager.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace ContactManager.Application.Services
{
    public interface IImageService
    {
        public Task<ImageDto> SaveImage(IFormFile file, string userId);
        public Task<bool> DeleteImage(string imageId);
        public Task<ImageDto?> GetImageById(string imageId);
    }
}
