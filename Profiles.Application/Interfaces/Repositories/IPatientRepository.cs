using Profiles.Application.Features.Patient.Queries;
using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories
{
    public interface IPatientRepository : IGenericRepository<PatientEntity>
    {
        Task<PatientEntity> GetMatchAsync(GetMatchedPatientQuery request);
        Task<(IEnumerable<PatientEntity> patients, int totalCount)> GetPatients(GetPatientsQuery request);
    }
}
