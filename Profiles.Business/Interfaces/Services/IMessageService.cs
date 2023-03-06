using Shared.Core.Enums;
using Shared.Messages;

namespace Profiles.Business.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendUpdateAccountStatusMessageAsync(Guid accountId, AccountStatuses status, string updaterId);
        Task SendDeletePhotoMessageAsync(Guid photoId);
        Task SendUpdatePatientMessageAsync(Guid id, string firstName, string lastName, string middleName, DateOnly dateOfBirth, string phoneNumber);
        Task SendUpdateDoctorMessageAsync(Guid id, string firstName, string lastName, string middleName, string specializationName);
    }
}
