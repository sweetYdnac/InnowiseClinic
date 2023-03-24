using Documents.Business.DTOs;
using Shared.Models;

namespace Documents.Business.Interfaces
{
    public interface IFileGeneratorService
    {
        Task<PdfResult> GetPdfAppointmentResult(PdfResultDTO dto);
    }
}
