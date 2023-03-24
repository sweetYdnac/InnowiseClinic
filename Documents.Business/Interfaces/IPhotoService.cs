using Microsoft.AspNetCore.Http;
using Shared.Models.Response.Documents;

namespace Documents.Business.Interfaces
{
    public interface IPhotoService
    {
        Task<BlobResponse> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(IFormFile file);
        Task UpdateAsync(Guid id, IFormFile file);
        Task DeleteAsync(Guid id);
    }
}
