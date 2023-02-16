using AutoMapper;
using MassTransit;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Receptionist;
using Profiles.Data.DTOs.ReceptionistSummary;
using Serilog;
using Shared.Exceptions;
using Shared.Messages;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Business.Implementations.Services
{
    public class ReceptionistsService : IReceptionistsService
    {
        private readonly IReceptionistsRepository _receptionistsRepository;
        private readonly IReceptionistSummaryRepository _receptionistSummaryRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public ReceptionistsService(
            IReceptionistsRepository receptionistsRepository,
            IReceptionistSummaryRepository receptionistSummaryRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint) =>
            (_receptionistsRepository, _receptionistSummaryRepository, _mapper, _publishEndpoint) =
            (receptionistsRepository, receptionistSummaryRepository, mapper, publishEndpoint);

        public async Task<ReceptionistResponse> GetByIdAsync(Guid id)
        {
            var response = await _receptionistsRepository.GetByIdAsync(id);

            return response is null
                    ? throw new NotFoundException($"Receptionist's profile with id = {id} doesn't exist.")
                    : response;
        }

        public async Task<GetReceptionistsResponseModel> GetPagedAsync(GetReceptionistsDTO dto)
        {
            var repositoryResponse = await _receptionistsRepository.GetPagedAsync(dto);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no receptionists in storage.");
            }

            return new GetReceptionistsResponseModel(
                repositoryResponse.receptionists,
                dto.PageNumber,
                dto.PageSize,
                repositoryResponse.totalCount);
        }

        public async Task<Guid?> CreateAsync(CreateReceptionistDTO dto)
        {
            var result = await _receptionistsRepository.AddAsync(dto);

            if (result > 0)
            {
                var receptionistSummary = _mapper.Map<CreateReceptionistSummaryDTO>(dto);
                result = await _receptionistSummaryRepository.AddAsync(receptionistSummary);

                if (result == 0)
                {
                    Log.Information("ReceptionistSummary wasn't added. {@receptionistSummary}", receptionistSummary);
                }

                return dto.Id;
            }
            else
            {
                Log.Information("Receptionist wasn't added. {@dto}", dto);

                return null;
            }
        }

        public async Task UpdateAsync(Guid id, UpdateReceptionistDTO dto)
        {
            var result = await _receptionistsRepository.UpdateAsync(id, dto);

            if (result > 0)
            {
                var receptionistSummary = _mapper.Map<UpdateReceptionistSummaryDTO>(dto);
                result = await _receptionistSummaryRepository.UpdateAsync(id, receptionistSummary);

                if (result == 0)
                {
                    Log.Information("ReceptionistSummary wasn't updated. {@id} {@receptionistSummary}", id, receptionistSummary);
                }
                else
                {
                    var accountId = await _receptionistsRepository.GetAccountIdAsync(id);

                    await _publishEndpoint.Publish(new AccountStatusUpdatedMessage
                    {
                        AccountId = accountId,
                        Status = dto.Status,
                        UpdaterId = dto.UpdaterId,
                    });
                }
            }
            else
            {
                Log.Information("Receptionist wasn't updated. {@id} {@dto}", id, dto);
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var photoId = await _receptionistsRepository.GetPhotoIdAsync(id);
            var result = await _receptionistsRepository.RemoveAsync(id);

            if (result > 0)
            {
                await _publishEndpoint.Publish(new ProfileDeletedMessage { PhotoId = photoId });

                result = await _receptionistSummaryRepository.RemoveAsync(id);

                if (result == 0)
                {
                    Log.Information("ReceptionistSummary wasn't removed. {@id}", id);
                }
            }
            else
            {
                throw new NotFoundException($"Receptionist's profile with id = {id} doesn't exist.");
            }
        }

        public async Task ChangeStatus(Guid id, ChangeStatusDTO dto)
        {
            var result = await _receptionistSummaryRepository.ChangeStatus(id, dto.Status);

            if (result > 0)
            {
                var accountId = await _receptionistsRepository.GetAccountIdAsync(id);

                await _publishEndpoint.Publish(new AccountStatusUpdatedMessage
                {
                    AccountId = accountId,
                    Status = dto.Status,
                    UpdaterId = dto.UpdaterId,
                });
            }
            else
            {
                throw new NotFoundException($"Receptionist's profile with id = {id} doesn't exist.");
            }
        }
    }
}
