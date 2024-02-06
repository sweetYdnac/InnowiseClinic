﻿using Profiles.Data.DTOs.Doctor;
using Shared.Models;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Data.Interfaces.Repositories
{
    public interface IDoctorsRepository
    {
        Task<DoctorResponse> GetByIdAsync(Guid Id);
        Task<PagedResult<DoctorInformationResponse>> GetDoctors(GetDoctorsDTO dto);
        Task AddAsync(CreateDoctorDTO dto);
        Task<int> UpdateAsync(Guid id, UpdateDoctorDTO dto);
        Task<int> RemoveAsync(Guid id);
        Task<Guid> GetPhotoIdAsync(Guid id);
        Task<int> SetInactiveStatusAsync(Guid specializationId);
        Task<int> UpdateSpecializationName(Guid specializationId, string specializationName);
    }
}
