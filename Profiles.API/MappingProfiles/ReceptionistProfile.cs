using AutoMapper;
using Profiles.Data.DTOs;
using Profiles.Data.DTOs.Receptionist;
using Profiles.Data.DTOs.ReceptionistSummary;
using Shared.Models.Request.Profiles;
using Shared.Models.Request.Profiles.Receptionist;

namespace Profiles.API.MappingProfiles
{
    public class ReceptionistProfile : Profile
    {
        public ReceptionistProfile()
        {
            CreateMap<GetReceptionistsRequest, GetReceptionistsDTO>();
            CreateMap<CreateReceptionistRequest, CreateReceptionistDTO>();
            CreateMap<UpdateReceptionistRequest, UpdateReceptionistDTO>();
            CreateMap<ChangeStatusRequestModel, ChangeStatusDTO>();

            CreateMap<CreateReceptionistDTO, CreateReceptionistSummaryDTO>();
            CreateMap<UpdateReceptionistDTO, UpdateReceptionistSummaryDTO>();
        }
    }
}
