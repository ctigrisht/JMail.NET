using MongoDB.Bson.Serialization.Attributes;

namespace JMail.Relay.Models
{
    public class JsonWebToken
    {
        [BsonId][BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Key { get; set; }
        public DateTime Issued { get; set; }
    }
}
