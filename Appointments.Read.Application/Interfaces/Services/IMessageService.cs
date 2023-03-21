namespace Appointments.Read.Application.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendGeneratePdfMessageAsync(Guid id, byte[] content);
    }
}
