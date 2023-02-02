using AutoMapper;
using MediatR;
using Offices.Application.DTOs;
using Offices.Application.Interfaces.Repositories;

namespace Offices.Application.Features.Office.Queries
{
    public class ChangeOfficeStatusCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class ChangeOfficeStatusCommandHandler : IRequestHandler<ChangeOfficeStatusCommand, Unit>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public ChangeOfficeStatusCommandHandler(IOfficeRepository officeRepository, IMapper mapper) =>
            (_officeRepository, _mapper) = (officeRepository, mapper);

        public async Task<Unit> Handle(ChangeOfficeStatusCommand request, CancellationToken cancellationToken)
        {
            await _officeRepository.ChangeStatusAsync(_mapper.Map<ChangeOfficeStatusDTO>(request));
            return Unit.Value;
        }
    }
}
