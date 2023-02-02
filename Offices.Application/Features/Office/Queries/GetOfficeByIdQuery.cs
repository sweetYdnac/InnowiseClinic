using AutoMapper;
using MediatR;
using Offices.Application.Interfaces.Repositories;
using Shared.Exceptions;
using Shared.Models.Response.Offices;

namespace Offices.Application.Features.Office.Queries
{
    public class GetOfficeByIdQuery : IRequest<OfficeDetailsResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetOfficeByIdQueryHandler : IRequestHandler<GetOfficeByIdQuery, OfficeDetailsResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public GetOfficeByIdQueryHandler(IOfficeRepository officeRepository, IMapper mapper) =>
            (_officeRepository, _mapper) = (officeRepository, mapper);

        public async Task<OfficeDetailsResponse> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
        {
            var office = await _officeRepository.GetByIdAsync(request.Id);

            return office is null
                ? throw new NotFoundException($"Office with id = {request.Id} doesn't exist.")
                : _mapper.Map<OfficeDetailsResponse>(office);
        }
    }
}
