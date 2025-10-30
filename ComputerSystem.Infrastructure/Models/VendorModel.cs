using System;
using System.Collections.Generic;

namespace ComputerSystem.Infrastructure.Models
{
    public class VendorModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public ICollection<ComponentModel> Components { get; set; } = new List<ComponentModel>();
    }
}
