using MassTransit;
using Profiles.Business.Interfaces.Services;
using Shared.Core.Enums;
using Shared.Messages;

namespace Profiles.Business.Implementations.Services
{
    public class MessageService : IMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public MessageService(ISendEndpointProvider sendEndpointProvider) => _sendEndpointProvider = sendEndpointProvider;

        public async Task SendUpdateAccountStatusMessageAsync(Guid accountId, AccountStatuses status, string updaterId)
        {
            var message = new UpdateAccountStatusMessage
            {
                AccountId = accountId,
                Status = status,
                UpdaterId = updaterId,
            };

            await _sendEndpointProvider.Send(message);
        }

        public async Task SendDeletePhotoMessageAsync(Guid photoId) =>
            await _sendEndpointProvider.Send(new DeletePhotoMessage { PhotoId = photoId });

        public async Task SendUpdatePatientMessageAsync(Guid id, string firstName, string lastName, string middleName, DateOnly dateOfBirth, string phoneNumber)
        {
            var message = new UpdatePatientMessage
            {
                Id = id,
                FullName = $"{firstName} {lastName} {middleName}",
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
            };

            await _sendEndpointProvider.Send(message);
        }

        public async Task SendUpdateDoctorMessageAsync(Guid id, string firstName, string lastName, string middleName, string specializationName, Guid officeId)
        {
            var message = new UpdateDoctorMessage
            {
                Id = id,
                FullName = $"{firstName} {lastName} {middleName}",
                SpecializationName = specializationName,
                OfficeId = officeId,
            };

            await _sendEndpointProvider.Send(message);
        }
    }
}
