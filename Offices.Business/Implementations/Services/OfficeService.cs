using AutoMapper;
using MassTransit;
using Offices.Business.Interfaces.Services;
using Offices.Data.DTOs;
using Offices.Data.Interfaces.Repositories;
using Shared.Exceptions;
using Shared.Messages;
using Shared.Models.Response.Offices;

namespace Offices.Business.Implementations.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public OfficeService(IOfficeRepository officeRepository, IMapper mapper , IPublishEndpoint publishEndpoint) =>
            (_officeRepository, _mapper, _publishEndpoint) = (officeRepository, mapper, publishEndpoint);

        public async Task ChangeStatus(ChangeOfficeStatusDTO dto)
        {
            var result = await _officeRepository.ChangeStatusAsync(dto);

            if (result > 0)
            {
                if (!dto.IsActive)
                {
                    await _publishEndpoint.Publish(new OfficeDisabledMessage { OfficeId = dto.Id });
                }
            }
            else
            {
                throw new NotFoundException($"Office with id = {dto.Id} doesn't exist.");
            }
        }

        public async Task<Guid> CreateAsync(CreateOfficeDTO dto)
        {
            await _officeRepository.AddAsync(dto);

            return dto.Id;
        }

        public async Task<OfficeResponse> GetByIdAsync(Guid id)
        {
            var office = await _officeRepository.GetByIdAsync(id);

            return office ?? throw new NotFoundException($"Office with id = {id} doesn't exist.");
        }

        public async Task<GetOfficesResponse> GetOfficesAsync(GetPagedOfficesDTO dto)
        {
            var result = await _officeRepository.GetPagedOfficesAsync(dto);

            return new GetOfficesResponse(
                result.Items,
                dto.CurrentPage,
                dto.PageSize,
                result.TotalCount);
        }

        public async Task UpdateAsync(Guid id, UpdateOfficeDTO dto)
        {
            var result = await _officeRepository.UpdateAsync(id, dto);

            if (result > 0)
            {
                var message = _mapper.Map<OfficeUpdatedMessage>(dto);
                message.OfficeId = id;

                await _publishEndpoint.Publish(message);
            }
            else
            {
                throw new NotFoundException($"Office with id = {id} doesn't exist.");
            }
        }
    }
}
