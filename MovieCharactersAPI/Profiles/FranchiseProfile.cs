using AutoMapper;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Franchise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(fdto => fdto.Movies, opt => opt
                .MapFrom(c => c.Movies.Select(m => m.MovieId).ToList()));

            CreateMap<FranchiseEditDTO, Franchise>();

            //CreateMap<FranchiseCreateDTO, Franchise>();
        }

    }
}
