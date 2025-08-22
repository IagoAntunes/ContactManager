using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ContactManager.Domain.Entities
{
    public class ContactEntity
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public required string Name { get; set; }


        [BsonElement("Description")]
        public string? Description { get; set; }


        [BsonElement("Phone")]
        public required string Phone { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("ImageId")]
        public string? ImageId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("UserId")]
        public required string UserId { get; set; }

    }
}
