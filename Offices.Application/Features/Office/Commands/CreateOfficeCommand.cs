using AutoMapper;
using MediatR;
using Offices.Application.DTOs;
using Offices.Application.Interfaces.Repositories;

namespace Offices.Application.Features.Office.Queries
{
    public class CreateOfficeCommand : IRequest<Guid?>
    {
        public Guid PhotoId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string RegistryPhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, Guid?>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public CreateOfficeCommandHandler(IOfficeRepository officeRepository, IMapper mapper) =>
            (_officeRepository, _mapper) = (officeRepository, mapper);

        public async Task<Guid?> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            return await _officeRepository.CreateAsync(_mapper.Map<CreateOfficeDTO>(request));
        }
    }
}
