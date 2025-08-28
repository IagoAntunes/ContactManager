using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using MongoDB.Driver;

namespace ContactManager.Infrastructure.Repositories
{
    internal class ImageRepository : IImageRepository
    {
        private readonly IMongoCollection<ImageMetadataEntity> _imagesCollection;
        public ImageRepository(IMongoDatabase mongoDatabase)
        {
            _imagesCollection = mongoDatabase.GetCollection<ImageMetadataEntity>("Images");
        }

        public async Task<bool> DeleteImageAsync(string imageId)
        {
            await _imagesCollection.DeleteOneAsync(i => i.Id == imageId);
            return true;
        }

        public async Task<ImageMetadataEntity> GetImageByIdAsync(string imageId)
        {
            var imageFinded = await _imagesCollection.Find(i => i.Id == imageId).FirstOrDefaultAsync();
            return imageFinded;
        }

        public async Task<ImageMetadataEntity> UploadImageAsync(ImageMetadataEntity image)
        {
            await _imagesCollection.InsertOneAsync(image);
            return image;
        }

    }
}
