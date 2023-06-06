using Shared.Messages;

namespace Offices.Business.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendDisableOfficeMessageAsync(Guid officeId);
        Task SendUpdateOfficeMessageAsync(Guid officeId, string city, string street, string houseNumber, string officeNumber, bool isActive);
        Task SendAddLogMessageAsync(AddLogMessage message);
    }
}
