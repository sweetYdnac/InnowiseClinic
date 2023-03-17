namespace Documents.Business.Interfaces
{
    public interface IAppointmentResultsService : IBlobService
    {
        Task UpdateOrCreateAsync(Guid id, string bytes);
    }
}
