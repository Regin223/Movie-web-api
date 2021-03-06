using System.Collections.Generic;

namespace MovieCharactersAPI.Model.DTO.Character
{
    /// <summary>
    /// Class <c>CharacterReadDTO</c> represent the model for a CharacterReadDTO with necessary properties. 
    /// Used when returning a character to the user instead of returning the actual character. 
    /// </summary>
    public class CharacterReadDTO
    {
        public int CharacterId { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
        public List<int> Movies { get; set; }

    }
}
