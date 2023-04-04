using Documents.Business.Configurations;
using Documents.Business.DTOs;
using Documents.Business.Interfaces;
using HandlebarsDotNet;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Shared.Models;

namespace Documents.Business.Implementations
{
    public class FileGeneratorService : IFileGeneratorService
    {
        private readonly PdfTemplateConfiguration _configuration;

        public FileGeneratorService(PdfTemplateConfiguration configuration) => _configuration = configuration;

        public async Task<PdfResult> GetPdfAppointmentResult(PdfResultDTO dto)
        {
            var handleBars = Handlebars.Create();
            handleBars.RegisterHelper("formatDate", (writer, context, parameters) => {
                var date = ((PdfResultDTO)context.Value).Date.ToString("yyyy-MM-dd HH:mm");
                writer.WriteSafeString(date);
            });

            var html = await File.ReadAllTextAsync(_configuration.HtmlPath);
            var template = handleBars.Compile(html);
            var parsed = template(dto);

            using (var stream = new MemoryStream())
            {
                using (var pdf = new PdfDocument(new PdfWriter(stream)))
                {
                    var properties = new ConverterProperties();
                    HtmlConverter.ConvertToPdf(parsed, pdf, properties);

                    return new PdfResult
                    {
                        Bytes = stream.ToArray(),
                        ContentType = "application/pdf",
                    };
                }
            }
        }
    }
}
