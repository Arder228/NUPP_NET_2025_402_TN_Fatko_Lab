using System;

namespace ComputerSystem.Common
{
    public class Cooling : Component
    {
        public string Type { get; set; } = string.Empty;
        public int FanCount { get; set; }
        public double MaxTdpWatts { get; set; }

        public Cooling(string model, string manufacturer, string type, int fanCount, double maxTdp)
            : base(model, manufacturer)
        {
            Type = type;
            FanCount = fanCount;
            MaxTdpWatts = maxTdp;
        }

        public Cooling() { }

        public Cooling(string model, string manufacturer, string type)
            : this(model, manufacturer, type, 1, 150) { }

        public override string GetInfo()
        {
            return $"Cooling: {Manufacturer} {Model}, Type: {Type}, Fans: {FanCount}, MaxTDP: {MaxTdpWatts}W";
        }
    }
}