using AutoMapper;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Doctor;
using Profiles.Data.DTOs.DoctorSummary;
using Profiles.Data.Interfaces.Repositories;
using Serilog;
using Shared.Exceptions;
using Shared.Messages;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Business.Implementations.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly IDoctorSummaryRepository _doctorSummaryRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public DoctorsService(
            IDoctorsRepository doctorsRepository,
            IDoctorSummaryRepository doctorSummaryRepository,
            IMessageService messageService,
            IMapper mapper) =>
        (_doctorsRepository, _doctorSummaryRepository, _messageService, _mapper) =
        (doctorsRepository, doctorSummaryRepository, messageService, mapper);

        public async Task<DoctorResponse> GetByIdAsync(Guid id)
        {
            var response = await _doctorsRepository.GetByIdAsync(id);

            return response ?? throw new NotFoundException($"Doctor's profile with id = {id} doesn't exist.");
        }

        public async Task<PagedResponse<DoctorInformationResponse>> GetPagedAndFilteredAsync(GetDoctorsDTO dto)
        {
            var result = await _doctorsRepository.GetDoctors(dto);

            return new PagedResponse<DoctorInformationResponse>(
                result.Items,
                dto.CurrentPage,
                dto.PageSize,
                result.TotalCount);
        }

        public async Task<Guid> CreateAsync(CreateDoctorDTO dto)
        {
            await _doctorsRepository.AddAsync(dto);
            await _doctorSummaryRepository.AddAsync(_mapper.Map<CreateDoctorSummaryDTO>(dto));

            return dto.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateDoctorDTO dto)
        {
            var result = await _doctorsRepository.UpdateAsync(id, dto);

            if (result > 0)
            {
                var accountId = await _doctorsRepository.GetAccountIdAsync(id);

                await _doctorSummaryRepository.UpdateAsync(id, _mapper.Map<UpdateDoctorSummaryDTO>(dto));

                var message = _mapper.Map<UpdateDoctorMessage>(dto);
                message.Id = id;
                await _messageService.SendUpdateDoctorMessageAsync(message);

                await _messageService.SendUpdateAccountStatusMessageAsync(accountId, dto.Status, dto.UpdaterId);
            }
            else
            {
                Log.Warning("Doctor wasn't updated. {@Id} {@Dto}", id, dto);
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var photoId = await _doctorsRepository.GetPhotoIdAsync(id);
            var result = await _doctorsRepository.RemoveAsync(id);

            if (result > 0)
            {
                await _messageService.SendDeletePhotoMessageAsync(photoId);
                await _doctorSummaryRepository.RemoveAsync(id);
            }
            else
            {
                throw new NotFoundException($"Doctor's profile with id = {id} doesn't exist.");
            }
        }

        public async Task ChangeStatusAsync(Guid id, ChangeStatusDTO dto)
        {
            var result = await _doctorSummaryRepository.ChangeStatus(id, dto.Status);

            if (result > 0)
            {
                var accountId = await _doctorsRepository.GetAccountIdAsync(id);
                await _messageService.SendUpdateAccountStatusMessageAsync(accountId, dto.Status, dto.UpdaterId);
            }
            else
            {
                throw new NotFoundException($"Doctor's profile with id = {id} doesn't exist.");
            }
        }

        public async Task SetInactiveStatusAsync(Guid specializationId)
        {
            var result = await _doctorsRepository.SetInactiveStatusAsync(specializationId);

            if (result == 0)
            {
                Log.Warning("There are no doctors with {@SpecializationId}", specializationId);
            }
        }

        public async Task UpdateSpecializationName(Guid specializationId, string specializationName)
        {
            var result = await _doctorsRepository.UpdateSpecializationName(specializationId, specializationName);

            if (result == 0)
            {
                Log.Warning("There are no doctors with {@SpecializationId}.", specializationId);
            }
        }
    }
}
