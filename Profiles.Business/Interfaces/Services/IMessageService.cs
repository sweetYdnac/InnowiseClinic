using Shared.Core.Enums;
using Shared.Messages;

namespace Profiles.Business.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendUpdateAccountStatusMessageAsync(Guid accountId, AccountStatuses status, string updaterId);
        Task SendDeletePhotoMessageAsync(Guid photoId);
        Task SendUpdatePatientMessageAsync(UpdatePatientMessage message);
        Task SendUpdateDoctorMessageAsync(UpdateDoctorMessage message);
        Task SendCreateAccountEmailAsync(SendCreateAccountEmailMessage message);
    }
}
