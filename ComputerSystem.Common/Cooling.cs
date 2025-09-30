using System;

namespace ComputerSystem.Common
{
    public class Cooling : Component
    {
        public string Type { get; set; }
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

        public override string GetInfo()
        {
            return $"Cooling: {Manufacturer} {Model}, Type: {Type}, Fans: {FanCount}, MaxTDP: {MaxTdpWatts}W";
        }
    }
}
