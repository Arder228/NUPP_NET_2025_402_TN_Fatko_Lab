using System;
using System.Collections.Generic;

namespace ComputerSystem.Infrastructure.Models
{
    public class ComputerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ComponentModel> Components { get; set; } = new List<ComponentModel>();

        public ComputerDetailModel? Detail { get; set; }
    }
}
