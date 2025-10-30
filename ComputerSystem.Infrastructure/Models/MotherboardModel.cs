namespace ComputerSystem.Infrastructure.Models
{
    public class MotherboardModel : ComponentModel
    {
        public string Chipset { get; set; } = string.Empty;
        public string FormFactor { get; set; } = string.Empty;
        public string Socket { get; set; } = string.Empty;
    }
}
