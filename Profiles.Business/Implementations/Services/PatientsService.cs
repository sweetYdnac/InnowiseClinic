using Profiles.Business.Interfaces.Repositories;
using Profiles.Business.Interfaces.Services;
using Shared.Exceptions;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Business.Implementations.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsService(IPatientsRepository patientRepository) => _patientsRepository = patientRepository;

        public async Task<PatientResponse> GetByIdAsync(Guid id)
        {
            var patient = await _patientsRepository.GetByIdAsync(id);

            return patient is null 
                ? throw new NotFoundException($"Patient profile with id = {id} doesn't exist.") 
                : patient;
        }
    }
}
