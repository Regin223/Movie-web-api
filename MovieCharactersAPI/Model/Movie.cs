using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Model
{
    public class Movie
    {

        public int MovieId { get; set; }

        [Required]
        [MaxLength(200)]
        public string MovieTitle { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        public int ReleaseYear { get; set; }

        [MaxLength(50)]
        public string Director { get; set; }

        [MaxLength(2083)]
        public string Trailer { get; set; }

        public ICollection<Character> Characters { get; set; }

        public int FranchiseId { get; set; }

        public Franchise Franchise { get; set; }
    }
}
