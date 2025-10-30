namespace ComputerSystem.Infrastructure.Models
{
    public class ProcessorModel : ComponentModel
    {
        public int Cores { get; set; }
        public double BaseFrequencyGhz { get; set; }
        public double BoostFrequencyGhz { get; set; }
    }
}
