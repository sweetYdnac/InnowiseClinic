using Shared.Models.Response.Documents;

namespace Documents.Business.Interfaces
{
    public interface IBlobService
    {
        Task<BlobResponse> GetBlobAsync(string name);
        Task<Guid> UploadBlobAsync(Guid id, string bytes);
        Task DeleteBlobAsync(Guid id);
        Task UpdateBlobAsync(Guid id, string bytes);
    }
}
