using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Model
{
    /// <summary>
    /// Class <c>Franchise</c> represent the model for a Franchise with necessary properties 
    /// according to the assignment instruction
    /// </summary>
    public class Franchise
    {
        public int FranchiseId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Movie> Movies { get; set; }

    }
}
