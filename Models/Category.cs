using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace testProjectApis.Models
{
    public class Category
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }
        [BsonElement("catrgoryName"), BsonRepresentation(BsonType.String)]
        public string? CatrgoryName { get; set; }
        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string? Description { get; set; }
        [BsonElement("image"), BsonRepresentation(BsonType.String)]
        public string? Image { get; set; }
        [BsonElement("imageUrl"), BsonRepresentation(BsonType.String)]
        public string? ImageUrl { get; set; }
        
        
    }
}
