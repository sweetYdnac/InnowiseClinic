using AutoMapper;
using Profiles.Business.Interfaces.Repositories;
using Profiles.Business.Interfaces.Services;
using Profiles.Data.DTOs.Doctor;
using Profiles.Data.DTOs.DoctorSummary;
using Serilog;
using Shared.Exceptions;
using Shared.Models.Response.Profiles.Doctor;

namespace Profiles.Business.Implementations.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly IDoctorSummaryRepository _doctorSummaryRepository;
        private readonly IMapper _mapper;

        public DoctorsService(IDoctorsRepository doctorsRepository, IDoctorSummaryRepository doctorSummaryRepository, IMapper mapper) =>
            (_doctorsRepository, _doctorSummaryRepository, _mapper) = (doctorsRepository, doctorSummaryRepository, mapper);

        public async Task<DoctorResponse> GetByIdAsync(Guid id)
        {
            var response = await _doctorsRepository.GetByIdAsync(id);

            return response is null
                ? throw new NotFoundException($"Doctor's profile with id = {id} doesn't exist.")
                : response;
        }

        public async Task<GetDoctorsResponseModel> GetPagedAndFilteredAsync(GetDoctorsDTO dto)
        {
            var repositoryResponse = await _doctorsRepository.GetDoctors(dto);

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no doctors in storage.");
            }

            return new GetDoctorsResponseModel(
                repositoryResponse.doctors, 
                dto.PageNumber, 
                dto.PageSize, 
                repositoryResponse.totalCount);
        }

        public async Task<Guid?> CreateAsync(CreateDoctorDTO dto)
        {
            var result = await _doctorsRepository.AddAsync(dto);

            if (result == 0)
            {
                Log.Information("Doctor wasn't added. {@dto}", dto);
                return null;
            }
            else
            {
                var doctorSummary = _mapper.Map<DoctorSummaryDTO>(dto);
                result = await _doctorSummaryRepository.AddAsync(doctorSummary);

                if (result == 0)
                {
                    Log.Information("DoctorSummary wasn't added. {@dto}", dto);
                }

                return dto.Id;
            }
        }
    }
}
