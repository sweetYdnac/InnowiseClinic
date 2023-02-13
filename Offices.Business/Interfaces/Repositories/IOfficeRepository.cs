﻿using Offices.Data.DTOs;
using Shared.Models.Response.Offices;

namespace Offices.Business.Interfaces.Repositories
{
    public interface IOfficeRepository
    {
        Task<OfficeResponse> GetByIdAsync(Guid id);
        Task<(IEnumerable<OfficeInformationResponse> offices, int totalCount)> GetPagedOfficesAsync(GetPagedOfficesDTO dto);
        Task<int> CreateAsync(CreateOfficeDTO dto);
        Task<int> ChangeStatusAsync(ChangeOfficeStatusDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateOfficeDTO dto);
    }
}
