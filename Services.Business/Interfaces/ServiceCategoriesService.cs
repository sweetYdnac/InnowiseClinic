using AutoMapper;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models.Response.Services.ServiceCategories;

namespace Services.Business.Interfaces
{
    public class ServiceCategoriesService : IServiceCategoriesService
    {
        private readonly IServiceCategoriesRepository _serviceCategoriesRepository;
        private readonly IMapper _mapper;

        public ServiceCategoriesService(IServiceCategoriesRepository serviceCategoriesRepository, IMapper mapper) =>
            (_serviceCategoriesRepository, _mapper) = (serviceCategoriesRepository, mapper);

        public async Task<ServiceCategoryResponse> GetByIdAsync(Guid id)
        {
            var response = await _serviceCategoriesRepository.GetByIdAsync(id);

            return response is null
                ? throw new NotFoundException($"Service category with id = {id} doesn't exist.")
                : _mapper.Map<ServiceCategoryResponse>(response);
        }

        public async Task<GetCategoriesResponse> GetAllAsync()
        {
            var response = await _serviceCategoriesRepository.GetAllAsync();

            return new GetCategoriesResponse
            {
                Categories = _mapper.Map<IEnumerable<ServiceCategoryResponse>>(response)
            };
        }
    }
}
