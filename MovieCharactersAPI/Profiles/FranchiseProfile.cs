using AutoMapper;
using MovieCharactersAPI.Model;
using MovieCharactersAPI.Model.DTO.Franchise;
using System.Linq;

namespace MovieCharactersAPI.Profiles
{
    /// <summary>
    /// Class <c>FranchiseProfile</c> inherits from Profile and is used to map a franchise object to a
    /// franchiseDTO object and the reverse case.  
    /// </summary>
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(fdto => fdto.Movies, opt => opt
                .MapFrom(c => c.Movies.Select(m => m.MovieId).ToList()));

            CreateMap<FranchiseEditDTO, Franchise>();

            CreateMap<FranchiseCreateDTO, Franchise>();
        }

    }
}
