using System;

namespace ComputerSystem.Common
{
    public class GraphicsCard : Component
    {
        public int MemoryGB { get; set; }
        public double CoreClockGHz { get; set; }

        public GraphicsCard(string model, string manufacturer, int memoryGB, double coreClockGHz)
            : base(model, manufacturer)
        {
            MemoryGB = memoryGB;
            CoreClockGHz = coreClockGHz;
        }

        public GraphicsCard() { }

        public override string GetInfo()
        {
            return $"GPU: {Manufacturer} {Model}, {MemoryGB}GB, {CoreClockGHz}GHz";
        }
    }
}