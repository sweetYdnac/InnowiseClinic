using AutoMapper;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Receptionist;
using Profiles.Data.DTOs.ReceptionistSummary;
using Profiles.Data.Interfaces.Repositories;
using Serilog;
using Shared.Exceptions;
using Shared.Models.Response;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Business.Implementations.Services
{
    public class ReceptionistsService : IReceptionistsService
    {
        private readonly IReceptionistsRepository _receptionistsRepository;
        private readonly IReceptionistSummaryRepository _receptionistSummaryRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ReceptionistsService(
            IReceptionistsRepository receptionistsRepository,
            IReceptionistSummaryRepository receptionistSummaryRepository,
            IMessageService messageService,
            IMapper mapper) =>
        (_receptionistsRepository, _receptionistSummaryRepository, _messageService, _mapper) =
        (receptionistsRepository, receptionistSummaryRepository, messageService, mapper);

        public async Task<ReceptionistResponse> GetByIdAsync(Guid id)
        {
            var response = await _receptionistsRepository.GetByIdAsync(id);

            return response ?? throw new NotFoundException($"Receptionist's profile with id = {id} doesn't exist.");
        }

        public async Task<PagedResponse<ReceptionistInformationResponse>> GetPagedAsync(GetReceptionistsDTO dto)
        {
            var result = await _receptionistsRepository.GetPagedAsync(dto);

            return new PagedResponse<ReceptionistInformationResponse>(
                result.Items,
                dto.CurrentPage,
                dto.PageSize,
                result.TotalCount);
        }

        public async Task<Guid> CreateAsync(CreateReceptionistDTO dto)
        {
            await _receptionistsRepository.AddAsync(dto);
            await _receptionistSummaryRepository.AddAsync(_mapper.Map<CreateReceptionistSummaryDTO>(dto));

            return dto.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateReceptionistDTO dto)
        {
            var result = await _receptionistsRepository.UpdateAsync(id, dto);

            if (result > 0)
            {
                await _messageService.SendUpdateAccountStatusMessageAsync(id, dto.Status, dto.UpdaterId);

                var receptionistSummary = _mapper.Map<UpdateReceptionistSummaryDTO>(dto);
                await _receptionistSummaryRepository.UpdateAsync(id, receptionistSummary);
            }
            else
            {
                Log.Warning("Receptionist wasn't updated {@Id} {@Dto}", id, dto);
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var photoId = await _receptionistsRepository.GetPhotoIdAsync(id);
            var result = await _receptionistsRepository.RemoveAsync(id);

            if (result > 0)
            {
                if (photoId is not null)
                {
                    await _messageService.SendDeletePhotoMessageAsync(photoId.Value);
                }

                await _receptionistSummaryRepository.RemoveAsync(id);
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
                await _messageService.SendUpdateAccountStatusMessageAsync(id, dto.Status, dto.UpdaterId);
            }
            else
            {
                throw new NotFoundException($"Receptionist's profile with id = {id} doesn't exist.");
            }
        }
    }
}
