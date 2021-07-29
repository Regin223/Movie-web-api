using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Model.DTO.Character
{
    /// <summary>
    /// Class <c>CharacterEditDTO</c> represent the model for a CharacterEditDTO with necessary properties. 
    /// Used when a user edit a character. 
    /// </summary>
    public class CharacterEditDTO
    {
        public int CharacterId { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}
