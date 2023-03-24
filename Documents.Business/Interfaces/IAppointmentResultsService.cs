using Shared.Models;
using Shared.Models.Response.Documents;

namespace Documents.Business.Interfaces
{
    public interface IAppointmentResultsService
    {
        Task<BlobResponse> GetByNameAsync(Guid id);
        Task CreateAsync(Guid id, PdfResult pdf);
    }
}
