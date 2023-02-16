using AutoMapper;
using MassTransit;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Doctor;
using Profiles.Data.DTOs.DoctorSummary;
using Serilog;
using Shared.Exceptions;
using Shared.Messages;
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

            return response is null
                ? throw new NotFoundException($"Doctor's profile with id = {id} doesn't exist.")
                : response;
        }

        public async Task<GetDoctorsResponseModel> GetPagedAndFilteredAsync(GetDoctorsDTO dto)
        {
            var repositoryResponse = await _doctorsRepository.GetDoctors(dto);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no doctors in storage.");
            }

            return new GetDoctorsResponseModel(
                repositoryResponse.doctors,
                dto.PageNumber,
                dto.PageSize,
                repositoryResponse.totalCount);
        }

        public async Task<Guid?> CreateAsync(CreateDoctorDTO dto)
        {
            var result = await _doctorsRepository.AddAsync(dto);

            if (result > 0)
            {
                var doctorSummary = _mapper.Map<CreateDoctorSummaryDTO>(dto);
                result = await _doctorSummaryRepository.AddAsync(doctorSummary);

                if (result == 0)
                {
                    Log.Information("DoctorSummary wasn't added. {@doctorSummary}", doctorSummary);
                }

                return dto.Id;
            }
            else
            {
                Log.Information("Doctor wasn't added. {@dto}", dto);

                return null;
            }
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

                var doctorSummary = _mapper.Map<UpdateDoctorSummaryDTO>(dto);
                result = await _doctorSummaryRepository.UpdateAsync(id, doctorSummary);

                if (result == 0)
                {
                    Log.Information("DoctorSummary wasn't updated. {@id} {@doctorSummary}", id, doctorSummary);
                }
            }
            else
            {
                Log.Information("Doctor wasn't updated. {@id} {@dto}", id, dto);
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var result = await _doctorsRepository.RemoveAsync(id);

            if (result > 0)
            {
                var photoId = await _doctorsRepository.GetPhotoIdAsync(id);

                await _publishEndpoint.Publish(new ProfileDeletedMessage { PhotoId = photoId });

                result = await _doctorSummaryRepository.RemoveAsync(id);

                if (result == 0)
                {
                    Log.Information("DoctorSummary wasn't deleted. {@id}", id);
                }
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

                await _publishEndpoint.Publish(new AccountStatusUpdatedMessage
                {
                    AccountId = accountId,
                    Status = dto.Status,
                    UpdaterId = dto.UpdaterId,
                });
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
                Log.Information("There are no doctors with {@specializationId}.", specializationId);
            }
        }

        public async Task UpdateSpecializationName(Guid specializationId, string specializationName)
        {
            var result = await _doctorsRepository.UpdateSpecializationName(specializationId, specializationName);

            if (result == 0)
            {
                Log.Information("There are no doctors with {@specializationId}.", specializationId);
            }
        }
    }
}
