using AutoMapper;
using Services.Business.Interfaces;
using Services.Data.DTOs;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models.Response;
using Shared.Models.Response.Services.Specialization;

namespace Services.Business.Implementations
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IMapper _mapper;

        public SpecializationService(IRepository<Specialization> specializationRepository, IMapper mapper) =>
            (_specializationRepository, _mapper) = (specializationRepository, mapper);

        public async Task<SpecializationResponse> GetByIdAsync(Guid id)
        {
            var response = await _specializationRepository.GetByIdAsync(id);

            return response is null
                ? throw new NotFoundException($"Specialization with id = {id} doesn't exist.")
                : _mapper.Map<SpecializationResponse>(response);
        }

        public async Task<Guid> CreateAsync(CreateSpecializationDTO dto)
        {
            var entity = _mapper.Map<Specialization>(dto);
            await _specializationRepository.AddAsync(entity);

            return entity.Id;
        }

        public async Task<PagedResponse<SpecializationResponse>> GetPagedAsync(GetSpecializationsDTO dto)
        {
            var response = await _specializationRepository.GetPagedAndFilteredAsync(dto.CurrentPage, dto.PageSize);

            return new(
                _mapper.Map<IEnumerable<SpecializationResponse>>(response.Items),
                dto.CurrentPage,
                dto.PageSize,
                response.TotalCount);
        }
    }
}
