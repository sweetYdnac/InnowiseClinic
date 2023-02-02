using AutoMapper;
using Offices.Application.DTOs;
using Offices.Application.Features.Office.Queries;
using Offices.Domain.Entities;
using Shared.Models.Response.Offices;

namespace Offices.Application.MappingProfiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<GetOfficesQuery, GetPagedOfficesDTO>();
            CreateMap<CreateOfficeCommand, CreateOfficeDTO>();

            CreateMap<OfficeEntity, OfficePreviewResponse>();
            CreateMap<OfficeEntity, OfficeDetailsResponse>();
        }
    }
}
