using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OfficesService.Domain.Models
{
    public class Photo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string Url { get; set; }
    }
}
