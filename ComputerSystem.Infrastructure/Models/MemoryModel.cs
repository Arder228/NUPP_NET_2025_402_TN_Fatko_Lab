namespace ComputerSystem.Infrastructure.Models
{
    public class MemoryModel : ComponentModel
    {
        public int SizeGB { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
