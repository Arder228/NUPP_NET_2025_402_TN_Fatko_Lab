using System.ComponentModel.DataAnnotations;

namespace ComputerSystem.REST.Models
{
    public class ComputerDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; }
        
        public List<ComponentDto> Components { get; set; } = new();
    }

    public abstract class ComponentDto
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Model { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Manufacturer { get; set; } = string.Empty;
        
        public int? VendorId { get; set; }
        public string? VendorName { get; set; }
    }

    public class ProcessorDto : ComponentDto
    {
        public int Cores { get; set; }
        public double BaseFrequencyGhz { get; set; }
        public double BoostFrequencyGhz { get; set; }
    }

    public class GraphicsCardDto : ComponentDto
    {
        public int MemoryGB { get; set; }
        public double CoreClockGhz { get; set; }
    }

    public class MemoryDto : ComponentDto
    {
        public int SizeGB { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    public class CoolingDto : ComponentDto
    {
        public string Type { get; set; } = string.Empty;
        public int FanCount { get; set; }
        public double MaxTdpWatts { get; set; }
    }

    public class MotherboardDto : ComponentDto
    {
        public string Chipset { get; set; } = string.Empty;
        public string FormFactor { get; set; } = string.Empty;
        public string Socket { get; set; } = string.Empty;
    }

    public class PowerSupplyDto : ComponentDto
    {
        public int PowerWatts { get; set; }
        public string EfficiencyRating { get; set; } = string.Empty;
    }

    public class CreateComputerRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateComputerRequest
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}