namespace Services.Business.Interfaces
{
    public interface IMessageService
    {
        Task SendDisableSpecializationMessageAsync(Guid specializationId);
    }
}
