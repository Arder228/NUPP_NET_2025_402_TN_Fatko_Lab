namespace ComputerSystem.Infrastructure.Models
{
    public class PowerSupplyModel : ComponentModel
    {
        public int PowerWatts { get; set; }
        public string EfficiencyRating { get; set; } = string.Empty;
    }
}
