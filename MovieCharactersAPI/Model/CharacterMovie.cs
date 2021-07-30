
namespace MovieCharactersAPI.Model
{
    /// <summary>
    /// Class <c>CharacterMovie</c> represents the linking table in the database with a many to many relationship.
    /// </summary>
    public class CharacterMovie
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }

    }
}
