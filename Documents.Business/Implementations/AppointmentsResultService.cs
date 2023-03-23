using Documents.Business.Interfaces;
using Documents.Data.Interfaces;
using Shared.Models;
using Shared.Models.Response.Documents;

namespace Documents.Business.Implementations
{
    public class AppointmentsResultService : IAppointmentResultsService
    {
        private readonly IAppointmentResultsRepository _appointmentResultsRepository;

        public AppointmentsResultService(IAppointmentResultsRepository appointmentResultsRepository) =>
            _appointmentResultsRepository = appointmentResultsRepository;

        public Task<BlobResponse> GetByNameAsync(Guid id)
        {
            return _appointmentResultsRepository.GetBlobAsync(id);
        }

        public async Task CreateAsync(Guid id, PdfResult pdf)
        {
            using (var stream = new MemoryStream(pdf.Bytes))
            {
                await _appointmentResultsRepository.AddOrUpdateBlobAsync(id, stream, pdf.ContentType);
            }
        }
    }
}
