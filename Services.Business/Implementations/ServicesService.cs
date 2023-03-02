using AutoMapper;
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
        private readonly IMapper _mapper;

        public ServicesService(IServicesRepository servicesRepository, IMapper mapper) =>
            (_servicesRepository, _mapper) = (servicesRepository, mapper);

        public async Task<ServiceResponse> GetByIdAsync(Guid id)
        {
            var response = await _servicesRepository.GetByIdAsync(id, s => s.Category);

            return response is null
                ? throw new NotFoundException($"Service with id = {id} doesn't exist.")
                : _mapper.Map<ServiceResponse>(response);
        }

        public async Task<PagedResponse<ServiceResponse>> GetPagedAsync(GetServicesDTO dto)
        {
            var response = await _servicesRepository.GetPagedAndFilteredAsync(
                dto.CurrentPage,
                dto.PageSize,
                new Expression<Func<Service, object>>[]
                {
                    s => s.Category,
                });

            return new(
            _mapper.Map<IEnumerable<ServiceResponse>>(response.Items),
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

            await _servicesRepository.UpdateAsync(entity);
        }
    }
}
