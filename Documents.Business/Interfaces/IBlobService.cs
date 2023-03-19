using Shared.Models.Response.Documents;

namespace Documents.Business.Interfaces
{
    public interface IBlobService
    {
        Task<BlobResponse> GetBlobAsync(string name);
        Task<Guid> AddOrUpdateBlobAsync(Guid id, string bytes, string contentType);
        Task DeleteBlobAsync(Guid id);
    }
}
