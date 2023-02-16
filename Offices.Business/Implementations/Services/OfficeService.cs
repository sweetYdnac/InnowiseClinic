using AutoMapper;
using MassTransit;
using Offices.Business.Interfaces.Repositories;
using Offices.Business.Interfaces.Services;
using Offices.Data.DTOs;
using Serilog;
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

        public async Task<Guid?> CreateAsync(CreateOfficeDTO dto)
        {
            var result = await _officeRepository.CreateAsync(dto);

            if (result == 0)
            {
                Log.Information("Office wasn't created; {@dto}", dto);
                return null;
            }
            else
            {
                return dto.Id;
            }
        }

        public async Task<OfficeResponse> GetByIdAsync(Guid id)
        {
            var office = await _officeRepository.GetByIdAsync(id);

            return office is null
                ? throw new NotFoundException($"Office with id = {id} doesn't exist.")
                : office;
        }

        public async Task<GetOfficesResponseModel> GetOfficesAsync(GetPagedOfficesDTO dto)
        {
            var repositoryResponse = await _officeRepository.GetPagedOfficesAsync(dto);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no offices in storage");
            }

            var response = new GetOfficesResponseModel(
                repositoryResponse.offices, 
                dto.PageNumber, 
                dto.PageSize, 
                repositoryResponse.totalCount
                );

            return response;
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
