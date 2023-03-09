using Appointments.Read.Application.DTOs.AppointmentResult;
using Appointments.Read.Application.Interfaces.Services;
using DinkToPdf;
using HandlebarsDotNet;
using Shared.Models.Response.Appointments.AppointmentResult;

namespace Appointments.Read.Persistence.Implementations.Services
{
    public class FileGeneratorService : IFileGeneratorService
    {
        //public PdfResultResponse GetPdfAppointmentResult(PdfResultDTO dto)
        //{
        //    var doc = new PdfDocument();
        //    var page = doc.Pages.Add();
        //    var pdfGrid = new PdfGrid();
        //    var data = new List<object>()
        //    {
        //        new { Id = "Full Name", Name = dto.PatientFullName },
        //        new { Id = "Patient date of birth", Name = dto.PatientDateOfBirth },
        //        new { Id = "Doctor full name", Name = dto.DoctorFullName },
        //        new { Id = "Doctor specialization", Name = dto.DoctorSpecializationName },
        //        new { Id = "Service", Name = dto.ServiceName },
        //        new { Id = "Complaints", Name = dto.Complaints },
        //        new { Id = "Conclusion", Name = dto.Conclusion },
        //        new { Id = "Recommendations", Name = dto.Recommendations },
        //    };

        //    pdfGrid.DataSource = data;
        //    pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 10));

        //    using (var stream = new MemoryStream())
        //    {
        //        doc.Save(stream);
        //        stream.Position = 0;
        //        doc.Close();

        //        return new PdfResultResponse
        //        {
        //            Content = stream.ToArray(),
        //            ContentType = "application/pdf",
        //            FileName = $"{dto.Date:hh:mm - yyyy-MM-dd} - {dto.PatientFullName}.pdf"
        //        };
        //    }
        //}

        public async Task<PdfResultResponse> GetPdfAppointmentResult(PdfResultDTO dto)
        {
            //var path = "/app/Appointments.Read.Application/FileTemplates/PdfTemplate.html";
            var path = "src/PdfTemplate.html";

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
                        WebSettings = { DefaultEncoding = "utf-8" },
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
