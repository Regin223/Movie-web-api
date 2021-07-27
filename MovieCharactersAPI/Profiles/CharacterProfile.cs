using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Character;

namespace MovieCharactersAPI.Profiles 
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(cdto => cdto.Movies, opt => opt
                .MapFrom(c => c.Movies.Select(m => m.MovieId).ToList()));

            CreateMap<CharacterEditDTO, Character>();

            CreateMap<CharacterCreateDTO, Character>();
        }
        

    }
}
