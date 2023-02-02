using AutoMapper;
using MediatR;
using Offices.Application.DTOs;
using Offices.Application.Interfaces.Repositories;
using Serilog;
using Shared.Models.Response.Offices;

namespace Offices.Application.Features.Office.Queries
{
    public class GetOfficesQuery : IRequest<GetOfficesResponseModel>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetOfficesQueryHandler : IRequestHandler<GetOfficesQuery, GetOfficesResponseModel>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;

        public GetOfficesQueryHandler(IOfficeRepository officeRepository, IMapper mapper) =>
            (_officeRepository, _mapper) = (officeRepository, mapper);

        public async Task<GetOfficesResponseModel> Handle(GetOfficesQuery request, CancellationToken cancellationToken)
        {
            var repositoryResponse = await _officeRepository.GetPagedOffices(_mapper.Map<GetPagedOfficesDTO>(request));

            if (repositoryResponse.totalCount == 0)
            {
                Log.Information("There are no offices in storage");
            }

            var offices = _mapper.Map<IEnumerable<OfficePreviewResponse>>(repositoryResponse.offices);
            var response = new GetOfficesResponseModel(offices, request.PageNumber, request.PageSize, repositoryResponse.totalCount);
            return response;
        }
    }
}
