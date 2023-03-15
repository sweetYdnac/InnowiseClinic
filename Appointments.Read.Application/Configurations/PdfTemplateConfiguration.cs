using Microsoft.Extensions.Configuration;

namespace Appointments.Read.Application.Configurations
{
    public class PdfTemplateConfiguration
    {
        public string HtmlPath { get; init; }
        public string CssPath { get; init; }

        public PdfTemplateConfiguration(IConfiguration configuration)
        {
            HtmlPath = configuration.GetValue<string>("PdfTemplate:HtmlPath");
            CssPath = configuration.GetValue<string>("PdfTemplate:CssPath");
        }
    }
}
