using Appointments.Read.Application.DTOs.AppointmentResult;
using Shared.Models.Response.Appointments.AppointmentResult;

namespace Appointments.Read.Application.Interfaces.Services
{
    public interface IFileGeneratorService
    {
        Task<PdfResultResponse> GetPdfAppointmentResult(PdfResultDTO dto);
    }
}
