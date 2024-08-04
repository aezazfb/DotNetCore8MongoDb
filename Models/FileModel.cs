using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace testProjectApis.Models
{
    public class FileModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FileName")]
        public string FileName { get; set; }

        [BsonElement("FileType")]
        public string FileType { get; set; }

        [BsonElement("FileContent")]
        public byte[] FileContent { get; set; }
    }
}
