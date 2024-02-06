using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Business.Interfaces;
using Services.Data.DTOs.Service;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models.Response;
using Shared.Models.Response.Services.Service;
using System.Linq.Expressions;

namespace Services.Business.Implementations
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ServicesService(IServicesRepository servicesRepository, IMessageService messageService, IMapper mapper) =>
            (_servicesRepository, _messageService, _mapper) = (servicesRepository, messageService, mapper);

        public async Task<ServiceResponse> GetByIdAsync(Guid id)
        {
            var response = await _servicesRepository.GetByIdAsync(id, s => s.Category);

            return response is null
                ? throw new NotFoundException($"Service with id = {id} doesn't exist.")
                : _mapper.Map<ServiceResponse>(response);
        }

        public async Task<PagedResponse<ServiceInformationResponse>> GetPagedAsync(GetServicesDTO dto)
        {
            var response = await _servicesRepository.GetPagedAndFilteredAsync(
                dto.CurrentPage,
                dto.PageSize,
                new Expression<Func<Service, object>>[]
                {
                    s => s.Category,
                },
                new Expression<Func<Service, bool>>[]
                {
                    s => EF.Functions.Like(s.Title, $"%{dto.Title}%"),
                    s => EF.Functions.Like(s.SpecializationId.ToString(), $"%{dto.SpecializationId}%"),
                    s => EF.Functions.Like(s.CategoryId.ToString(), $"%{dto.CategoryId}%"),
                    s => dto.IsActive == null || s.IsActive.Equals(dto.IsActive),
                });

            return new(
            _mapper.Map<IEnumerable<ServiceInformationResponse>>(response.Items),
            dto.CurrentPage,
            dto.PageSize,
            response.TotalCount);
        }

        public async Task<Guid> CreateAsync(CreateServiceDTO dto)
        {
            var entity = _mapper.Map<Service>(dto);
            await _servicesRepository.AddAsync(entity);

            return entity.Id;
        }

        public async Task ChangeStatusAsync(Guid id, bool isActive) =>
            await _servicesRepository.ChangeStatusAsync(id, isActive);

        public async Task UpdateAsync(Guid id, UpdateServiceDTO dto)
        {
            var entity = _mapper.Map<Service>(dto);
            entity.Id = id;

            var result = await _servicesRepository.UpdateAsync(entity);

            if (result > 0)
            {
                await _messageService.SendUpdateServiceMessageAsync(id, dto.Title, dto.TimeSlotSize);
            }
        }
    }
}
