using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OfficesService.Domain.Models
{
    public class Office
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string Address { get; set; }
        public string PhoneNumber { get; set; }
        public required bool IsActive { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Photos { get; set; }
        [BsonIgnore]
        public List<Photo> PhotosList { get; set; }
    }
}
