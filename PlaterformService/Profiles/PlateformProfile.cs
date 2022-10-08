using AutoMapper;
using PlateformService.Dtos;
using PlatefromService.Models;
using PlaterformService.Dtos;

namespace PlaterformService.Profiles
{
    public class PlateformsProfile : Profile
    {
        public PlateformsProfile()
        {
            CreateMap<Plateform, PlateformReadDto>();
            CreateMap<PlateformCreateDto, Plateform>();
            CreateMap<PlateformReadDto, PlateformPublishDto>();
        }
    }
}
