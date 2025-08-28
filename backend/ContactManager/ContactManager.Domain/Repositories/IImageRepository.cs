using ContactManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Domain.Repositories
{
    public interface IImageRepository
    {
        public Task<ImageMetadataEntity> UploadImageAsync(ImageMetadataEntity image);
        public Task<bool> DeleteImageAsync(string imageId);
        public Task<ImageMetadataEntity> GetImageByIdAsync(string imageId);
    }
}
