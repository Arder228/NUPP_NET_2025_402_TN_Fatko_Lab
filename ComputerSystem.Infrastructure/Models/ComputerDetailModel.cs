namespace ComputerSystem.Infrastructure.Models
{
    public class ComputerDetailModel
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public int ComputerId { get; set; }
        public ComputerModel Computer { get; set; } = null!;
    }
}
