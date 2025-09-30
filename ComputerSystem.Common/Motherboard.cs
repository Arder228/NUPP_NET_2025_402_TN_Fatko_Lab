using System;

namespace ComputerSystem.Common
{
    public class Motherboard : Component
    {
        public string Chipset { get; set; }
        public string FormFactor { get; set; }
        public string Socket { get; set; }

        public Motherboard(string model, string manufacturer, string chipset, string formFactor, string socket)
            : base(model, manufacturer)
        {
            Chipset = chipset;
            FormFactor = formFactor;
            Socket = socket;
        }

        public Motherboard() { }

        public override string GetInfo()
        {
            return $"MB: {Manufacturer} {Model}, {Chipset}, {FormFactor}, Socket {Socket}";
        }
    }
}