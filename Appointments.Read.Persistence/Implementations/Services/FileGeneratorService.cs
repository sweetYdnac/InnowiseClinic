using Appointments.Read.Application.Configurations;
using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Interfaces.Services;
using HandlebarsDotNet;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Shared.Models.Response.Appointments.AppointmentResult;

namespace Appointments.Read.Persistence.Implementations.Services
{
    public class FileGeneratorService : IFileGeneratorService
    {
        private readonly PdfTemplateConfiguration _configuration;

        public FileGeneratorService(PdfTemplateConfiguration configuration) => _configuration = configuration;

        public async Task<PdfResultResponse> GetPdfAppointmentResult(PdfResultDTO dto)
        {
            var html = await File.ReadAllTextAsync(_configuration.HtmlPath);
            var handleBars = Handlebars.Compile(html);
            var parsed = handleBars(dto);

            using (var stream = new MemoryStream())
            {
                using (var pdf = new PdfDocument(new PdfWriter(stream)))
                {
                    var properties = new ConverterProperties();
                    HtmlConverter.ConvertToPdf(parsed, pdf, properties);

                    return new PdfResultResponse
                    {
                        Content = stream.ToArray(),
                        ContentType = "application/pdf",
                        FileName = $"{dto.Date:hh:mm - yyyy-MM-dd} - {dto.PatientFullName}.pdf"
                    };
                }
            }
        }
    }
}
