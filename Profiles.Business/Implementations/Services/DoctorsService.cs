using Profiles.Business.Interfaces.Repositories;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs.Doctor;
using Shared.Exceptions;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Business.Implementations.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepository _doctorsRepository;

        public DoctorsService(IDoctorsRepository doctorsRepository) => _doctorsRepository = doctorsRepository;

        public async Task<DoctorInformationResponse> GetByIdAsync(Guid id)
        {
            var response = await _doctorsRepository.GetByIdAsync(id);

            return response is null
                ? throw new NotFoundException($"Doctor's profile with id = {id} doesn't exist.")
                : response;
        }

        public Task<GetDoctorsResponseModel> GetPagedAndFilteredAsync(GetDoctorsDTO dto)
        {

        }
    }
}
