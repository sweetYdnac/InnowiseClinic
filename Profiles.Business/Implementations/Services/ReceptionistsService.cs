﻿using AutoMapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs.Receptionist;
using Profiles.Data.DTOs.ReceptionistSummary;
using Serilog;
using Shared.Exceptions;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Business.Implementations.Services
{
    public class ReceptionistsService : IReceptionistsService
    {
        private readonly IReceptionistsRepository _receptionistsRepository;
        private readonly IReceptionistSummaryRepository _receptionistSummaryRepository;
        private readonly IMapper _mapper;

        public ReceptionistsService(
            IReceptionistsRepository receptionistsRepository,
            IReceptionistSummaryRepository receptionistSummaryRepository,
            IMapper mapper) =>
            (_receptionistsRepository, _receptionistSummaryRepository, _mapper) = 
            (receptionistsRepository, receptionistSummaryRepository, mapper);

        public async Task<ReceptionistResponse> GetByIdAsync(Guid id)
        {
            var response = await _receptionistsRepository.GetByIdAsync(id);

            return response is null
                    ? throw new NotFoundException($"Receptionist's profile with id = {id} doesn't exist.")
                    : response;
        }

        public async Task<GetReceptionistsResponseModel> GetPagedAsync(GetReceptionistsDTO dto)
        {
            var repositoryResponse = await _receptionistsRepository.GetPagedAsync(dto);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no receptionists in storage.");
            }

            return new GetReceptionistsResponseModel(
                repositoryResponse.receptionists,
                dto.PageNumber,
                dto.PageSize,
                repositoryResponse.totalCount);
        }

        public async Task<Guid?> CreateAsync(CreateReceptionistDTO dto)
        {
            var result = await _receptionistsRepository.CreateAsync(dto);

            if (result > 0)
            {
                var receptionistSummary = _mapper.Map<CreateReceptionistSummaryDTO>(dto);
                result = await _receptionistSummaryRepository.CreateAsync(receptionistSummary);

                if (result == 0)
                {
                    Log.Information("ReceptionistSummary wasn't added. {@receptionistSummary}", receptionistSummary);
                }

                return dto.Id;
            }
            else
            {
                Log.Information("Receptionist wasn't added. {@dto}", dto);
                return null;
            }
        }

        public async Task UpdateAsync(Guid id, UpdateReceptionistDTO dto)
        {
            var result = await _receptionistsRepository.UpdateAsync(id, dto);

            if (result > 0)
            {
                var receptionistSummary = _mapper.Map<UpdateReceptionistSummaryDTO>(dto);
                result = await _receptionistSummaryRepository.UpdateAsync(id, receptionistSummary);

                if (result == 0)
                {
                    Log.Information("ReceptionistSummary wasn't updated. {@id} {@receptionistSummary}", id, receptionistSummary);
                }
            }
            else
            {
                Log.Information("Receptionist wasn't updated. {@id} {@dto}", id, dto);
            }
        }
    }
}
