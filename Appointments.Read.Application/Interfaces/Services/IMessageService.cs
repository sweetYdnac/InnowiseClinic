namespace Appointments.Read.Application.Interfaces.Services
{
    public interface IMessageService
    {
        Task SendGeneratePdfAsync(Guid id, byte[] content);
    }
}
