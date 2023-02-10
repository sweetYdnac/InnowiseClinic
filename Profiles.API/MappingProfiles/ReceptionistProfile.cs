using AutoMapper;
using Profiles.Data.DTOs.Receptionist;
using Profiles.Data.DTOs.ReceptionistSummary;
using Shared.Models.Request.Profiles.Receptionist;

namespace Profiles.API.MappingProfiles
{
    public class ReceptionistProfile : Profile
    {
        public ReceptionistProfile()
        {
            CreateMap<GetReceptionistsRequestModel, GetReceptionistsDTO>();
            CreateMap<CreateReceptionistRequestModel, CreateReceptionistDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(model => Guid.NewGuid()));
            CreateMap<CreateReceptionistDTO, CreateReceptionistSummaryDTO>();
            CreateMap<UpdateReceptionistRequestModel, UpdateReceptionistDTO>();
            CreateMap<UpdateReceptionistDTO, UpdateReceptionistSummaryDTO>();
        }
    }
}
