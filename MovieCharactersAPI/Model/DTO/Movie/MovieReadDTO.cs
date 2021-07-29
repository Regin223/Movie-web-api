using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Model.DTO.Movie
{
    /// <summary>
    /// Class <c>MovieReadDTO</c> represent the model for a MovieReadDTO with necessary properties. 
    /// Used when returning a movie to the user instead of returning the actual movie. 
    /// </summary>
    public class MovieReadDTO
    {
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Trailer { get; set; }
        public int? FranchiseId { get; set; }
        public List<int> Characters { get; set; }
    }
}
