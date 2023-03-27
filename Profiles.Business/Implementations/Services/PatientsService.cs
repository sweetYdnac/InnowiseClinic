using AutoMapper;
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
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public PatientsService(
            IPatientsRepository patientRepository,
            IMessageService messageService,
            IMapper mapper) =>
        (_patientsRepository, _messageService, _mapper) = (patientRepository, messageService, mapper);

        public async Task<PatientResponse> GetByIdAsync(Guid id)
        {
            var patient = await _patientsRepository.GetByIdAsync(id);

            return patient ?? throw new NotFoundException($"Patient's profile with id = {id} doesn't exist.");
        }

        public async Task<PagedResponse<PatientInformationResponse>> GetPagedAndFilteredAsync(GetPatientsDTO dto)
        {
            var result = await _patientsRepository.GetPatients(dto);

            return new PagedResponse<PatientInformationResponse>(
                result.Items,
                dto.CurrentPage,
                dto.PageSize,
                result.TotalCount);
        }

        public async Task<Guid> CreateAsync(CreatePatientDTO dto)
        {
            await _patientsRepository.AddAsync(dto);

            return dto.Id;
        }

        public async Task<PatientResponse> GetMatchedPatientAsync(GetMatchedPatientDTO dto) =>
            await _patientsRepository.GetMatchAsync(dto);

        public async Task RemoveAsync(Guid id)
        {
            var photoId = await _patientsRepository.GetPhotoIdAsync(id);
            var result = await _patientsRepository.RemoveAsync(id);

            if (result > 0)
            {
                await _messageService.SendDeletePhotoMessageAsync(photoId);
            }
            else
            {
                throw new NotFoundException($"Patient's profile with id = {id} doesn't exist.");
            }
        }

        public async Task UpdateAsync(Guid id, UpdatePatientDTO dto)
        {
            var result = await _patientsRepository.UpdateAsync(id, dto);

            if (result > 0)
            {
                await _messageService.SendUpdatePatientMessageAsync(_mapper.Map<UpdatePatientMessage>(dto));
            }
            else
            {
                Log.Warning("Patient wasn't updated {@Id} {@Dto}", id, dto);
            }
        }

        public async Task LinkToAccount(Guid id, Guid accountId)
        {
            var result = await _patientsRepository.LinkToAccount(id, accountId);

            if (result == 0)
            {
                Log.Warning("Patient wasn't linked to account {@Id} {@AccountId}", id, accountId);
            }
        }
    }
}
