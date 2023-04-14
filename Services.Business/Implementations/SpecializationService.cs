using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Business.Interfaces;
using Services.Data.DTOs.Specialization;
using Services.Data.Entities;
using Services.Data.Interfaces;
using Shared.Exceptions;
using Shared.Models.Response;
using Shared.Models.Response.Services.Specialization;
using System.Linq.Expressions;

namespace Services.Business.Implementations
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IServicesRepository _servicesRepository;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public SpecializationService(
            IRepository<Specialization> specializationRepository,
            IServicesRepository servicesRepository,
            IMessageService messageService,
            IMapper mapper) =>
        (_specializationRepository, _servicesRepository, _messageService, _mapper) =
        (specializationRepository, servicesRepository, messageService, mapper);

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
            var response = await _specializationRepository.GetPagedAndFilteredAsync(
                dto.CurrentPage,
                dto.PageSize,
                new Expression<Func<Specialization, bool>>[]
                {
                    s => EF.Functions.Like(s.Title, $"%{dto.Title}%"),
                    s => s.IsActive.Equals(dto.IsActive),
                });

            return new(
                _mapper.Map<IEnumerable<SpecializationResponse>>(response.Items),
                dto.CurrentPage,
                dto.PageSize,
                response.TotalCount);
        }

        public async Task ChangeStatusAsync(Guid id, bool isActive)
        {
            await _specializationRepository.ChangeStatusAsync(id, isActive);

            if (!isActive)
            {
                await DisableServicesAndDoctors(id);
            }
        }

        public async Task UpdateAsync(Guid id, UpdateSpecializationDTO dto)
        {
            var entity = _mapper.Map<Specialization>(dto);
            entity.Id = id;

            await _specializationRepository.UpdateAsync(entity);

            if (!dto.IsActive)
            {
                await DisableServicesAndDoctors(id);
            }
        }

        private async Task DisableServicesAndDoctors(Guid specializationId)
        {
            await _servicesRepository.DisableAsync(specializationId);
            await _messageService.SendDisableSpecializationMessageAsync(specializationId);
        }
    }
}
