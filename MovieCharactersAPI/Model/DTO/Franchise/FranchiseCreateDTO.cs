using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCharactersAPI.Model.DTO.Franchise
{
    /// <summary>
    /// Class <c>FranchiseCreateDTO</c> represent the model for a FranchiseCreateDTO with necessary properties. 
    /// Used when a user adding a new franchise. 
    /// </summary>
    public class FranchiseCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
