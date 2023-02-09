using AutoMapper;
using Profiles.Data.DTOs.Receptionist;
using Shared.Models.Request.Profiles.Receptionist;

namespace Profiles.API.MappingProfiles
{
    public class ReceptionistProfile : Profile
    {
        public ReceptionistProfile()
        {
            CreateMap<GetReceptionistsRequestModel, GetReceptionistsDTO>();
        }
    }
}
