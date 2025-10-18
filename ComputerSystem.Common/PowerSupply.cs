using System;

namespace ComputerSystem.Common
{
    public class PowerSupply : Component
    {
        public int PowerWatts { get; set; }
        public string EfficiencyRating { get; set; } = string.Empty;

        public PowerSupply(string model, string manufacturer, int powerWatts, string efficiencyRating)
            : base(model, manufacturer)
        {
            PowerWatts = powerWatts;
            EfficiencyRating = efficiencyRating;
        }

        public PowerSupply() { }

        public override string GetInfo()
        {
            return $"PSU: {Manufacturer} {Model}, {PowerWatts}W, {EfficiencyRating}";
        }

        public static PowerSupply CreateNew()
        {
            Console.Write("Введіть модель: ");
            string model = Console.ReadLine() ?? string.Empty;

            Console.Write("Введіть виробника: ");
            string manufacturer = Console.ReadLine() ?? string.Empty;

            Console.Write("Введіть потужність (Вт): ");
            int watts = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Введіть рейтинг ефективності (наприклад, 80+ Gold): ");
            string rating = Console.ReadLine() ?? string.Empty;

            return new PowerSupply(model, manufacturer, watts, rating);
        }
    }
}