using Documents.Business.Interfaces;
using Documents.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Models.Response.Documents;

namespace Documents.Business.Implementations
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotosRepository _photosRepository;

        public PhotoService(IPhotosRepository photosRepository) => _photosRepository = photosRepository;

        public async Task<BlobResponse> GetByIdAsync(Guid id)
        {
            return await _photosRepository.GetBlobAsync(id);
        }

        public async Task<Guid> CreateAsync(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var id = Guid.NewGuid();
                await _photosRepository.AddOrUpdateBlobAsync(id, stream, file.ContentType);

                return id;
            }
        }

        public async Task UpdateAsync(Guid id, IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                await _photosRepository.AddOrUpdateBlobAsync(id, stream, file.ContentType);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await _photosRepository.DeleteBlobAsync(id);
        }
    }
}
