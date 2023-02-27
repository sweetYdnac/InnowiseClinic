﻿using AutoMapper;
using Services.Data.DTOs.Service;
using Services.Data.Entities;
using Shared.Models.Request.Services.Service;
using Shared.Models.Response.Services.Service;

namespace Services.API.MappingProfiles
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            CreateMap<GetServicesRequest, GetServicesDTO>();
            CreateMap<CreateServiceRequest, CreateServiceDTO>();
            CreateMap<CreateServiceDTO, Service>()
                .ForMember(s => s.Id, opt => opt.MapFrom(dto => Guid.NewGuid()));
            CreateMap<UpdateServiceRequest, UpdateServiceDTO>();
            CreateMap<UpdateServiceDTO, Service>();

            CreateMap<Service, ServiceResponse>()
                .ForMember(r => r.CategoryTitle, opt => opt.MapFrom(s => s.Category.Title));

        }
    }
}
