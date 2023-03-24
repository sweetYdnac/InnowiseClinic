using AutoMapper;
using Documents.Business.DTOs;
using Shared.Messages;

namespace Documents.API.MappingProfiles
{
    public class AppointmentResultProfile : Profile
    {
        public AppointmentResultProfile()
        {
            CreateMap<GeneratePdfMessage, PdfResultDTO>();
        }
    }
}
