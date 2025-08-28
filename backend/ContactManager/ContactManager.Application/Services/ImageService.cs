using AutoMapper;
using ContactManager.Domain.Dtos;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using ContactManager.Domain.Result;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace ContactManager.Application.Services
{
    internal class ImageService : IImageService
    {
        private readonly IImageRepository repository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public ImageService(
            IImageRepository repository,
            IMapper mapper,
            IWebHostEnvironment environment
            )
        {
            this.repository = repository;
            this.mapper = mapper;
            this.environment = environment;
        }

        public async Task<bool> DeleteImage(string imageId)
        {
            var imageEntity = await repository.GetImageByIdAsync(imageId);

            if (imageEntity == null)
            {
                return true;
            }

            try
            {
                var filePath = Path.Combine("Uploads", imageEntity.StoredFileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            var deleteSuccess = await repository.DeleteImageAsync(imageId);

            if (!deleteSuccess)
            {
                return false;
            }

            return true;
        }

        public async Task<ImageDto?> GetImageById(string imageId)
        {
            var imageFinded = await repository.GetImageByIdAsync(imageId);
            var imageDto = mapper.Map<ImageDto>(imageFinded);
            return imageDto;
        }

        public async Task<ImageDto> SaveImage(IFormFile file, string userId)
        {
            var imageId = ObjectId.GenerateNewId().ToString();
            string? filePathOnDisk = null; // Caminho físico para possível exclusão

            try
            {
                filePathOnDisk = await SaveFileToDiskAsync(file, imageId);

                var imageEntity = new ImageMetadataEntity
                {
                    Id = imageId,
                    OriginalFileName = file.FileName,
                    StoredFileName = Path.GetFileName(filePathOnDisk),
                    StoredPath = Path.Combine("Images", Path.GetFileName(filePathOnDisk)),
                    ContentType = file.ContentType,
                    CreatedAt = DateTime.UtcNow
                };

                var resultUploadImage = await repository.UploadImageAsync(imageEntity);

                var createdImageDto = mapper.Map<ImageDto>(resultUploadImage);
                return createdImageDto;
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(filePathOnDisk) && File.Exists(filePathOnDisk))
                {
                    File.Delete(filePathOnDisk);
                }
               
                throw;
            }
        }

        private async Task<string?> SaveFileToDiskAsync(IFormFile file, string imageId)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("O arquivo não pode ser nulo ou vazio.", nameof(file));
            }

            const long maxFileSize = 5 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                throw new ArgumentException($"O tamanho do arquivo excede o limite de {maxFileSize / 1024 / 1024} MB.");
            }

            var uploadsFolder = Path.Combine(environment.ContentRootPath, "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var storedFileName = $"{imageId}{fileExtension}";
            var physicalFilePath = Path.Combine(uploadsFolder, storedFileName);

            var filePath = Path.Combine(uploadsFolder, storedFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return physicalFilePath;
        }
    }
}
