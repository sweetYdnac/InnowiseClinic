using Shared.Models.Response.Documents;

namespace Documents.Data.Interfaces
{
    public interface IBlobRepository
    {
        Task<BlobResponse> GetBlobAsync(Guid id);
        Task<Guid> AddOrUpdateBlobAsync(Guid id, Stream stream, string contentType);
        Task DeleteBlobAsync(Guid id);
    }
}
