using Profiles.Application.Features.Doctor.Queries;
using Profiles.Domain.Entities;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Application.Interfaces.Repositories
{
    public interface IDoctorRepository : IGenericRepository<DoctorEntity>
    {
    }
}
