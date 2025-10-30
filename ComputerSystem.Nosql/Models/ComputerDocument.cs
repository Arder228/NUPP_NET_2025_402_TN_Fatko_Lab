using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ComputerSystem.Nosql.Models
{
    public class ComputerDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Manufacturer")]
        public string Manufacturer { get; set; } = string.Empty;

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
