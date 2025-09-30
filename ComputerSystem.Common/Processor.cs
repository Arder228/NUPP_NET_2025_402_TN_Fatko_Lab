using System;

namespace ComputerSystem.Common
{
    public class Processor : Component
    {
        public int Cores { get; set; }
        public double BaseFrequencyGhz { get; set; }
        public double BoostFrequencyGhz { get; set; }
        public double FrequencyGHz { get; set; }

        public delegate void OverclockedHandler(object sender, OverclockEventArgs e);
        public event OverclockedHandler Overclocked;

        public Processor(string model, string manufacturer, int cores, double baseFreq, double boostFreq)
            : base(model, manufacturer)
        {
            Cores = cores;
            BaseFrequencyGhz = baseFreq;
            BoostFrequencyGhz = boostFreq;
        }

        public Processor() { }

        public void Overclock(double newFreqGhz)
        {
            if (newFreqGhz > BoostFrequencyGhz)
            {
                BoostFrequencyGhz = newFreqGhz;
                Overclocked?.Invoke(this, new OverclockEventArgs(newFreqGhz));
            }
        }

        public override string GetInfo()
        {
            return $"CPU: {Manufacturer} {Model}, {Cores}c, {BaseFrequencyGhz}GHz/{BoostFrequencyGhz}GHz";
        }
    }

    public class OverclockEventArgs : EventArgs
    {
        public double NewFrequency { get; }
        public OverclockEventArgs(double newFreq) => NewFrequency = newFreq;
    }
}