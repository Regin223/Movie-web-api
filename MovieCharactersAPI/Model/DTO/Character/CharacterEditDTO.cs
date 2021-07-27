using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Model.DTO.Character
{
    public class CharacterEditDTO
    {
        public int CharacterId { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}
