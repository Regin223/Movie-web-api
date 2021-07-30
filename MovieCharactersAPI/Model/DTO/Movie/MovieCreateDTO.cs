using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Model.DTO.Movie
{
    /// <summary>
    /// Class <c>MovieCreateDTO</c> represent the model for a MovieCreateDTO with necessary properties. 
    /// Used when a user adding a new movie. 
    /// </summary>
    public class MovieCreateDTO
    {
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Trailer { get; set; }
    }
}
