namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequest
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
