using AutoMapper;
using MovieApi.Dtos.Award;
using MovieApi.Models;

namespace MovieApi.Mappings
{
    public class AwardMappings : Profile
    {
        public AwardMappings()
        {
            CreateMap<Award, AwardDto>();
            CreateMap<AwardCreationDto, Award>();
            CreateMap<AwardUpdateDto, Award>();
        }
    }

}
