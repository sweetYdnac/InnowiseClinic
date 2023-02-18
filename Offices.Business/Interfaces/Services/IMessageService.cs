namespace Offices.Business.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendOfficeDisabledMessageAsync(Guid officeId);
        Task SendOfficeUpdatedMessageAsync(Guid officeId, string city, string street, string houseNumber, string officeNumber, bool isActive);
    }
}
