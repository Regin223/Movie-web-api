using System.Collections.Generic;

namespace MovieCharactersAPI.Model.DTO.Franchise
{
    /// <summary>
    /// Class <c>FranchiseReadDTO</c> represent the model for a FranchiseReadDTO with necessary properties. 
    /// Used when returning a franchise to the user instead of returning the actual franchise. 
    /// </summary>
    public class FranchiseReadDTO
    {
        public int FranchiseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Movies { get; set; }
    }
}
