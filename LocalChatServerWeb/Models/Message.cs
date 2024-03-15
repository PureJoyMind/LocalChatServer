using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;

namespace LocalChatServerWeb.Models
{
    [CollectionName("Messages")]
    public class Message : Document
    {
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public string Id { get; set; }

        // [BsonElement("AddedAtUtc")]
        // [JsonPropertyName("AddedAtUtc")]
        // public DateTime CreatedAt { get; set; }
        public Guid SessionId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
    }
}
