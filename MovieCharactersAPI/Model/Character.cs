using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Model
{
    public class Character
    {

        public int CharacterId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [MaxLength(50)]
        public string Alias { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; }

        [MaxLength(2083)]
        public string Picture { get; set; }

        public ICollection<Movie> Movies { get; set; }

    }
}
