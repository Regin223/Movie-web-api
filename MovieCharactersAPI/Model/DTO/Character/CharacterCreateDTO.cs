
namespace MovieCharactersAPI.Model.DTO.Character
{
    /// <summary>
    /// Class <c>CharacterCreateDTO</c> represent the model for a CharacterCreateDTO with necessary properties. 
    /// Used when a user adding a new character. 
    /// </summary>
    public class CharacterCreateDTO
    {
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}
