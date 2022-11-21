namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequest
    {
        public string Name { get; set; } = default!;
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
