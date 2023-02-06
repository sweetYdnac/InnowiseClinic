using Profiles.Application.Features.Patient.Queries;
using Profiles.Domain.Entities;

namespace Profiles.Application.Interfaces.Repositories
{
    public interface IPatientRepository : IGenericRepository<PatientEntity>
    {
        Task<PatientEntity> GetMatch(GetMatchedPatientQuery request);
    }
}
