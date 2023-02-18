using AutoMapper;
using MassTransit;
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
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public DoctorsService(
            IDoctorsRepository doctorsRepository,
            IDoctorSummaryRepository doctorSummaryRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint) =>
            (_doctorsRepository, _doctorSummaryRepository, _mapper, _publishEndpoint) =
            (doctorsRepository, doctorSummaryRepository, mapper, publishEndpoint);

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
                dto.PageNumber,
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

                await _publishEndpoint.Publish(new AccountStatusUpdatedMessage
                {
                    AccountId = accountId,
                    Status = dto.Status,
                    UpdaterId = dto.UpdaterId,
                });

                await _doctorSummaryRepository.UpdateAsync(id, _mapper.Map<UpdateDoctorSummaryDTO>(dto));
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
                await _publishEndpoint.Publish(new ProfileDeletedMessage { PhotoId = photoId });
                await _doctorSummaryRepository.RemoveAsync(id);
            }
            else
            {
                throw new NotFoundException("Doctor's profile with id = {id} doesn't exist.");
            }
        }

        public async Task ChangeStatusAsync(Guid id, ChangeStatusDTO dto)
        {
            var result = await _doctorSummaryRepository.ChangeStatus(id, dto.Status);

            if (result > 0)
            {
                var accountId = await _doctorsRepository.GetAccountIdAsync(id);

                await _publishEndpoint.Publish(new AccountStatusUpdatedMessage
                {
                    AccountId = accountId,
                    Status = dto.Status,
                    UpdaterId = dto.UpdaterId,
                });
            }
            else
            {
                throw new NotFoundException("Doctor's profile with id = {id} doesn't exist.");
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
