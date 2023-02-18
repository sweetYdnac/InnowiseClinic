using Profiles.Data.DTOs.Patient;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Business.Interfaces.Services
{
    public interface IPatientsService
    {
        Task<PatientResponse> GetByIdAsync(Guid id);
        Task<PagedResponse<PatientInformationResponse>> GetPagedAndFilteredAsync(GetPatientsDTO dto);
        Task<Guid> CreateAsync(CreatePatientDTO dto);
        Task<PatientResponse> GetMatchedPatientAsync(GetMatchedPatientDTO dto);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(Guid id, UpdatePatientDTO dto);
        Task LinkToAccount(Guid id, Guid accountId);
    }
}
