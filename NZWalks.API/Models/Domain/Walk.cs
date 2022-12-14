namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //navigation prop
        public Region Region { get; set; } = default!;
        public WalkDifficulty WalkDifficulty { get; set; } = default!;
    }
}
