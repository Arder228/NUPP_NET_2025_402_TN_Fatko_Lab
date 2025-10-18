using System.Text.Json.Serialization;

namespace ComputerSystem.Common
{
    [JsonDerivedType(typeof(Processor), typeDiscriminator: "processor")]
    [JsonDerivedType(typeof(GraphicsCard), typeDiscriminator: "graphicsCard")]
    [JsonDerivedType(typeof(Memory), typeDiscriminator: "memory")]
    [JsonDerivedType(typeof(Cooling), typeDiscriminator: "cooling")]
    [JsonDerivedType(typeof(Motherboard), typeDiscriminator: "motherboard")]
    [JsonDerivedType(typeof(PowerSupply), typeDiscriminator: "powerSupply")]
    public abstract class Component
    {
        private static int _createdCount = 0;
        public static int GetCreatedCount() => _createdCount;

        public Guid Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;

        public Component(string model, string manufacturer)
        {
            Id = Guid.NewGuid();
            Model = model;
            Manufacturer = manufacturer;
            _createdCount++;
        }

        public Component() 
        {
            Id = Guid.NewGuid();
        }

        public virtual string GetInfo()
        {
            return $"{Manufacturer} {Model} (Id: {Id})";
        }
    }
}