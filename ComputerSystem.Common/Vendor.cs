using System;

namespace ComputerSystem.Common
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Vendor(string name, string country)
        {
            Id = Guid.NewGuid();
            Name = name;
            Country = country;
        }

        public Vendor() 
        {
            Id = Guid.NewGuid();
        }
    }
}