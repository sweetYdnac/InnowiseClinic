using AutoMapper;
using Logs.Data.DTOs;
using Logs.Data.Entities;
using MongoDB.Bson;
using Shared.Messages;
using Shared.Models.Request.LogsAPI;
using Shared.Models.Response.Logs;

namespace Logs.API.MappingProfiles
{
    public class LogProfile: Profile
    {
        public LogProfile()
        {
            CreateMap<AddLogMessage, CreateLogDTO>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(m => ObjectId.GenerateNewId()));

            CreateMap<CreateLogDTO, Log>()
                .ForMember(ent => ent.DateTime, opt => opt.MapFrom(m => DateTime.Today));

            CreateMap<Log, LogResponse>();

            CreateMap<GetLogsRequest, GetLogsDTO>()
                .ForMember(dto => dto.Date, opt =>
                    opt.MapFrom(r => r.Date == null ? null : r.Date.Value.ToDateTime(TimeOnly.MinValue) as DateTime?));

            CreateMap<UpdateLogRequest, UpdateLogDTO>();

        }
    }
}
