using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Interfaces.Services;
using DinkToPdf;
using HandlebarsDotNet;
using Microsoft.Extensions.Configuration;
using Shared.Models.Response.Appointments.AppointmentResult;

namespace Appointments.Read.Persistence.Implementations.Services
{
    public class FileGeneratorService : IFileGeneratorService
    {
        private readonly IConfiguration _configuration;

        public FileGeneratorService(IConfiguration configuration) => _configuration = configuration;

        public async Task<PdfResultResponse> GetPdfAppointmentResult(PdfResultDTO dto)
        {
            var path = _configuration.GetValue<string>("PdfTemplate:HtmlPath");

            var html = await File.ReadAllTextAsync(path);
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
                            UserStyleSheet = _configuration.GetValue<string>("PdfTemplate:CssPath")
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
