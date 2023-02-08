using Profiles.Application.Features.Patient.Commands;
using Profiles.Application.Features.Patient.Queries;
using Profiles.Data.Entities;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Business.Interfaces.Repositories
{
    public interface IPatientsRepository
    {
        Task<PatientResponse> GetByIdAsync(Guid id);
        //Task<PatientEntity> GetMatchAsync(GetMatchedPatientQuery request);
        //Task<(IEnumerable<PatientEntity> patients, int totalCount)> GetPatients(GetPatientsQuery request);
        //Task LinkToAccount(LinkToAccountCommand request);
    }
}
