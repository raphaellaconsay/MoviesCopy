using AutoMapper;
using MovieApi.Dtos.Actor;
using MovieApi.Models;
using System.Globalization;

namespace MovieApi.Mappings
{
    public class ActorMappings : Profile
    {
        public ActorMappings()
        {
            CreateMap<Actor, ActorDto>()
                .ForMember(actordto => actordto.Birthday, opt => opt.MapFrom(actor => actor.Birthday.ToString("D")));

            CreateMap<ActorCreationDto, Actor>()
                .ForMember(actor => actor.Birthday, opt => opt.MapFrom(actorCDto => DateTime.ParseExact(actorCDto.Birthday!, "M-d-yyyy", CultureInfo.InvariantCulture)));

            CreateMap<ActorUpdateDto, Actor>();
        }
    }
}
