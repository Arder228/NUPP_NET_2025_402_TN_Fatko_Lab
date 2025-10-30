using System;

namespace ComputerSystem.Infrastructure.Models
{
    public abstract class ComponentModel
    {
        public int Id { get; set; }
        public Guid Uid { get; set; } = Guid.NewGuid();
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;

        public int? ComputerId { get; set; }
        public ComputerModel? Computer { get; set; }

        public int? VendorId { get; set; }
        public VendorModel? Vendor { get; set; }
    }
}
