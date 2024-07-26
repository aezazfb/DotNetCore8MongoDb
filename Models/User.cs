using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace testProjectApis.Models
{
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        [BsonElement("user_firstname"), BsonRepresentation(BsonType.String)]
        public string? FirstName { get; set; }
        [BsonElement("user_lastname"), BsonRepresentation(BsonType.String)]
        public string? LastName { get; set; }
        [BsonElement("user_email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }
        [BsonElement("user_password"), BsonRepresentation(BsonType.String)]
        public string? Password { get; set; }
        [BsonElement("user_role"), BsonRepresentation(BsonType.String)]
        public string? Role { get; set; }
    }
}
