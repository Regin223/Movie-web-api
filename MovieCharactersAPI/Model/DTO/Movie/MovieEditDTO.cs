
namespace MovieCharactersAPI.Model.DTO.Movie
{
    /// <summary>
    /// Class <c>MovieEditDTO</c> represent the model for a MovieEditDTO with necessary properties. 
    /// Used when a user edit a movie. 
    /// </summary>
    public class MovieEditDTO
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Trailer { get; set; }
        public int FranchiseId { get; set; }
    }
}
