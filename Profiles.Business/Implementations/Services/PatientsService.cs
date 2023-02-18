using MassTransit;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs.Patient;
using Profiles.Data.Interfaces.Repositories;
using Serilog;
using Shared.Exceptions;
using Shared.Messages;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Patient;

namespace Profiles.Business.Implementations.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public PatientsService(IPatientsRepository patientRepository, IPublishEndpoint publishEndpoint) =>
            (_patientsRepository, _publishEndpoint) = (patientRepository, publishEndpoint);

        public async Task<PatientResponse> GetByIdAsync(Guid id)
        {
            var patient = await _patientsRepository.GetByIdAsync(id);

            return patient is null
                ? throw new NotFoundException($"Patient's profile with id = {id} doesn't exist.")
                : patient;
        }

        public async Task<PagedResponse<PatientInformationResponse>> GetPagedAndFilteredAsync(GetPatientsDTO dto)
        {
            var result = await _patientsRepository.GetPatients(dto);

            if (result.TotalCount == 0)
            {
                Log.Information("There are no patients in storage.");
            }

            return new PagedResponse<PatientInformationResponse>(
                result.Items,
                dto.PageNumber,
                dto.PageSize,
                result.TotalCount);
        }

        public async Task<Guid?> CreateAsync(CreatePatientDTO dto)
        {
            var result = await _patientsRepository.AddAsync(dto);

            if (result > 0)
            {
                return dto.Id;
            }
            else
            {
                Log.Information("Entity wasn't created. {@entity}", dto);

                return null;
            }
        }

        public async Task<PatientResponse> GetMatchedPatientAsync(GetMatchedPatientDTO dto)
        {
            return await _patientsRepository.GetMatchAsync(dto);
        }

        public async Task DeleteAsync(Guid id)
        {
            var photoId = await _patientsRepository.GetPhotoIdAsync(id);
            var result = await _patientsRepository.RemoveAsync(id);

            if (result > 0)
            {
                await _publishEndpoint.Publish(new ProfileDeletedMessage { PhotoId = photoId });
            }
            else
            {
                Log.Information("Patient with {id} wasn't remove", id);
            }
        }

        public async Task UpdateAsync(Guid id, UpdatePatientDTO dto)
        {
            var result = await _patientsRepository.UpdateAsync(id, dto);

            if (result == 0)
            {
                Log.Information("Patient wasn't updated. {@id} {@entity}", id, dto);
            }
        }

        public async Task LinkToAccount(Guid id, Guid accountId)
        {
            var result = await _patientsRepository.LinkToAccount(id, accountId);

            if (result == 0)
            {
                Log.Information("Patient wasn't linked to account. {@id} {@request}", id, accountId);
            }
        }
    }
}
