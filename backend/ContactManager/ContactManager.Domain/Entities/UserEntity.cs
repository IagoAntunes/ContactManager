using MongoDB.Bson.Serialization.Attributes;

namespace ContactManager.Domain.Entities
{
    public class UserEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        public required string Name { get; set; }

        [BsonElement("Email")]
        public required string Email { get; set; }

        [BsonElement("PasswordHash")]
        public required string PasswordHash { get; set; }


    }
}
