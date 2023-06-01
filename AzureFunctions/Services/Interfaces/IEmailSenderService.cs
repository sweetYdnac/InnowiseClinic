using MimeKit;
using System.Threading.Tasks;

namespace AzureFunctions.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(MimeMessage emailMessage);
    }
}
