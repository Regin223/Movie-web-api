using System.Linq;
using AutoMapper;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Character;

namespace MovieCharactersAPI.Profiles 
{
    /// <summary>
    /// Class <c>CharacterProfile</c> inherits from Profile and is used to map a character object to a
    /// characterDTO object and the reverse case.  
    /// </summary>
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(cdto => cdto.Movies, opt => opt
                .MapFrom(c => c.CharacterMovies.Select(m => m.MovieId).ToList()));

            CreateMap<CharacterEditDTO, Character>();

            CreateMap<CharacterCreateDTO, Character>();
        }
        

    }
}
