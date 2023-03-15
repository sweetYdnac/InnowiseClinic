using Appointments.Read.Application.Configurations;
using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Interfaces.Services;
using DinkToPdf;
using HandlebarsDotNet;
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

            var converter = new SynchronizedConverter(new PdfTools());
            var document = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = parsed,
                        WebSettings =
                        {
                            DefaultEncoding = "utf-8",
                            UserStyleSheet = _configuration.CssPath,
                        },
                    }
                }
            };

            var pdfBytes = converter.Convert(document);

            return new PdfResultResponse
            {
                Content = pdfBytes,
                ContentType = "application/pdf",
                FileName = $"{dto.Date:hh:mm - yyyy-MM-dd} - {dto.PatientFullName}.pdf"
            };
        }
    }
}
