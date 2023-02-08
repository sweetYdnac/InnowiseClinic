using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Business.Interfaces.Services
{
    public interface IPatientsService
    {
        Task<PatientResponse> GetByIdAsync(Guid id);
    }
}
