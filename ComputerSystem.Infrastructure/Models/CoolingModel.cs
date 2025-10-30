namespace ComputerSystem.Infrastructure.Models
{
    public class CoolingModel : ComponentModel
    {
        public string Type { get; set; } = string.Empty;
        public int FanCount { get; set; }
        public double MaxTdpWatts { get; set; }
    }
}
