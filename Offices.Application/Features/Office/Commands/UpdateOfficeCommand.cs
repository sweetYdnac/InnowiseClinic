using AutoMapper;
using MediatR;
using Offices.Application.DTOs;
using Offices.Application.Interfaces.Repositories;

namespace Offices.Application.Features.Office.Queries
{
    public class UpdateOfficeCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string RegistryPhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommand, Unit>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public UpdateOfficeCommandHandler(IOfficeRepository officeRepository, IMapper mapper) =>
            (_officeRepository, _mapper) = (officeRepository, mapper);

        public async Task<Unit> Handle(UpdateOfficeCommand request, CancellationToken cancellationToken)
        {
            await _officeRepository.UpdateAsync(_mapper.Map<UpdateOfficeDTO>(request));
            return Unit.Value;
        }
    }
}
