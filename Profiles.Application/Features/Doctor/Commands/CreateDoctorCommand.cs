using AutoMapper;
using MediatR;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Domain.Entities;

namespace Profiles.Application.Features.Doctor.Commands
{
    public class CreateDoctorCommand : IRequest<Guid?>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid AccountId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid SpecializationId { get; set; }
        public Guid OfficeId { get; set; }
        public DateTime CareerStartYear { get; set; }
        public string SpecializationName { get; set; }
        public string OfficeAddress { get; set; }
    }

    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Guid?>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IGenericRepository<DoctorSummary> _doctorSummaryRepository;
        private readonly IMapper _mapper;
        public CreateDoctorCommandHandler(
            IDoctorRepository doctorRepository, 
            IMapper mapper,
            IGenericRepository<DoctorSummary> doctorSummaryRepository) =>
            (_doctorRepository, _mapper, _doctorSummaryRepository) = (doctorRepository, mapper, doctorSummaryRepository);

        public async Task<Guid?> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var id = await _doctorRepository.AddAsync(_mapper.Map<DoctorEntity>(request));

            if (id is not null)
            {
                var doctorSummary = _mapper.Map<DoctorSummary>(request);
                doctorSummary.Id = id.Value;

                await _doctorSummaryRepository.AddAsync(doctorSummary);
            }

            return id;
        }
    }
}
