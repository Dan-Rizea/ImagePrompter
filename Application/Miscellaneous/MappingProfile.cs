using Application.DTOs;
using AutoMapper;
using Persistence.Entities;

namespace Application.Miscellaneous
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Session, CurrentSessionDto>();
            CreateMap<SessionVersion, CurrentSessionVersionDto>();
        }
    }
}
