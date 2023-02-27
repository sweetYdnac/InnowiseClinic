using AutoMapper;
using Services.Business.Interfaces;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models.Response.Services.Service;

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
    }
}
