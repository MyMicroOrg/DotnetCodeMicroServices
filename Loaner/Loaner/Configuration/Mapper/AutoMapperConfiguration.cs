using AutoMapper;
using Loaner.Data;
using Loaner.Dtos;

namespace Loaner.Configuration.Mapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration(IServiceProvider serviceProvider)
        {
            CreateMap<Customer, CustomerDto>();
        }       
    }
}
