using System;

namespace ComputerSystem.Common
{
    public class Memory : Component
    {
        public int SizeGB { get; set; }
        public string Type { get; set; }

        public Memory(string model, string manufacturer, int sizeGB, string type)
            : base(model, manufacturer)
        {
            SizeGB = sizeGB;
            Type = type;
        }

        public Memory() { }

        public override string GetInfo()
        {
            return $"RAM: {Manufacturer} {Model}, {SizeGB}GB {Type}";
        }
    }
}