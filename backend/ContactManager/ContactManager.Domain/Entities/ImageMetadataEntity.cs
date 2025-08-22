using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContactManager.Domain.Entities
{
    public class ImageMetadataEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("OriginalFileName")]
        public required string OriginalFileName { get; set; }

        [BsonElement("StoredFileName")]
        public required string StoredFileName { get; set; }

        [BsonElement("StoredPath")]
        public required string StoredPath { get; set; }

        [BsonElement("ContentType")]
        public required string ContentType { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
