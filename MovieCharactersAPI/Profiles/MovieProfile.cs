using AutoMapper;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Movie;
using System.Linq;

namespace MovieCharactersAPI.Profiles
{
    /// <summary>
    /// Class <c>MovieProfile</c> inherits from Profile and is used to map a movie object to a movieDTO
    /// object and the reverse case.  
    /// </summary>   
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
