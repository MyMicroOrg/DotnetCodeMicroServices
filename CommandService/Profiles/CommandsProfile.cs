using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace Commandsprofile.Profiles
{

    public class CommandsProfile : Profile
    {

        public CommandsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlateformPublishedDto, Platform>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
        }
    }
}