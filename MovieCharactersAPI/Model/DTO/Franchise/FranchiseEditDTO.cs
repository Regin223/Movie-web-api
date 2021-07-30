
namespace MovieCharactersAPI.Model.DTO.Franchise
{
    /// <summary>
    /// Class <c>FranchiseEditDTO</c> represent the model for a FranchiseEditDTO with necessary properties. 
    /// Used when a user edit a franchise. 
    /// </summary>
    public class FranchiseEditDTO
    {
        public int FranchiseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
