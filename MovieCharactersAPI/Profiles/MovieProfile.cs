using AutoMapper;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(cdto => cdto.Characters, opt => opt
                .MapFrom(m => m.CharacterMovies.Select(m => m.CharacterId).ToList()));

            CreateMap<MovieEditDTO, Movie>();

            CreateMap<MovieCreateDTO, Movie>();
        }
    }
}
