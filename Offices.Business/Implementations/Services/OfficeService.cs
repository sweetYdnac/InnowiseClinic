﻿using Offices.Business.Interfaces.Repositories;
using Offices.Business.Interfaces.Services;
using Offices.Data.DTOs;
using Serilog;
using Shared.Exceptions;
using Shared.Models.Response;
using Shared.Models.Response.Offices;

namespace Offices.Business.Implementations.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IOfficeInformationRepository _officeInformationRepository;

        public OfficeService(IOfficeRepository officeRepository, IOfficeInformationRepository officeInformationRepository) =>
            (_officeRepository, _officeInformationRepository) = (officeRepository, officeInformationRepository);

        public async Task ChangeStatus(ChangeOfficeStatusDTO dto)
        {
            var result = await _officeRepository.ChangeStatusAsync(dto);

            if (result == 0)
            {
                throw new NotFoundException($"Office with id = {dto.Id} doesn't exist.");
            }
        }

        public async Task<Status201Response> CreateAsync(CreateOfficeDTO dto)
        {
            return await _officeRepository.CreateAsync(dto);
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
            var repositoryResponse = await _officeInformationRepository.GetPagedOfficesAsync(dto);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no offices in storage");
            }

            var response = new GetOfficesResponseModel(repositoryResponse.offices, dto.PageNumber, dto.PageSize, repositoryResponse.totalCount);
            return response;
        }

        public async Task UpdateAsync(Guid id, UpdateOfficeDTO dto)
        {
            var result = await _officeRepository.UpdateAsync(id, dto);

            if (result == 0)
            {
                throw new NotFoundException($"Office with id = {id} doesn't exist.");
            }
        }
    }
}
