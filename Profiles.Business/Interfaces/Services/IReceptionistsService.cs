﻿using Profiles.Data.DTOs.Receptionist;
using Shared.Models.Response.Profiles.Receptionist;

namespace Profiles.Business.Interfaces.Services
{
    public interface IReceptionistsService
    {
        Task<ReceptionistResponse> GetByIdAsync(Guid id);
        Task<GetReceptionistsResponseModel> GetPagedAsync(GetReceptionistsDTO dto);
    }
}
