using AutoMapper;
using Services.Business.Interfaces;
using Services.Data.DTOs;
using Services.Data.Entities;
using Services.Data.Interfaces;

namespace Services.Business.Implementations
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IGenericRepository<Specialization> _specializationRepository;
        private readonly IMapper _mapper;

        public SpecializationService(IGenericRepository<Specialization> specializationRepository, IMapper mapper) =>
            (_specializationRepository, _mapper) = (specializationRepository, mapper);

        public async Task<Guid> CreateAsync(CreateSpecializationDTO dto)
        {
            var entity = _mapper.Map<Specialization>(dto);
            await _specializationRepository.AddAsync(entity);

            return entity.Id;
        }
    }
}
