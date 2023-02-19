using Offices.Business.Interfaces.Services;
using Offices.Data.DTOs;
using Offices.Data.Interfaces.Repositories;
using Shared.Exceptions;
using Shared.Models.Response;
using Shared.Models.Response.Offices;

namespace Offices.Business.Implementations.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMessageService _messageService;

        public OfficeService(IOfficeRepository officeRepository, IMessageService messageService) =>
            (_officeRepository, _messageService) = (officeRepository, messageService);

        public async Task ChangeStatus(ChangeOfficeStatusDTO dto)
        {
            var result = await _officeRepository.ChangeStatusAsync(dto);

            if (result > 0)
            {
                if (!dto.IsActive)
                {
                    await _messageService.SendDisableOfficeMessageAsync(dto.Id);
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

        public async Task<PagedResponse<OfficeInformationResponse>> GetOfficesAsync(GetPagedOfficesDTO dto)
        {
            var result = await _officeRepository.GetPagedOfficesAsync(dto);

            return new PagedResponse<OfficeInformationResponse>(
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
                await _messageService.SendUpdateOfficeMessageAsync(id, dto.City, dto.Street, dto.HouseNumber, dto.OfficeNumber, dto.IsActive);
            }
            else
            {
                throw new NotFoundException($"Office with id = {id} doesn't exist.");
            }
        }
    }
}
