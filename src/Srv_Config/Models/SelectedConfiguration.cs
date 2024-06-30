using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;

namespace Srv_Config.Models
{
    public class SelectedConfiguration : IEntity
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        public string UserId { get; set; }
        public string Summary { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public SelectedConfiguration()
        {
            ObjectId = ObjectId.GenerateNewId();
            UserId = string.Empty;
            Summary = string.Empty;
        }

        public object GenerateNewID()
        {
            return ObjectId.GenerateNewId();
        }

        [BsonIgnore]
        public string ID
        {
            get => ObjectId.ToString();
            set => ObjectId = ObjectId.Parse(value);
        }

        public bool HasDefaultID()
        {
            return ObjectId == ObjectId.Empty;
        }

        public void BeforeSave()
        {
        }
    }
}
