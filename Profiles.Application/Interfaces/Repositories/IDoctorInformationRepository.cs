﻿using Profiles.Application.Features.Doctor.Queries;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Application.Interfaces.Repositories
{
    public interface IDoctorInformationRepository
    {
        Task<DoctorInformationResponse> GetByIdAsync(Guid Id);
        Task<(IEnumerable<DoctorInformationResponse> doctors, int totalCount)> GetDoctors(GetDoctorsInformationQuery request);
    }
}
