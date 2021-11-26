using MongoDB.Bson.Serialization.Attributes;

namespace JMail.Relay.Models
{
    public class VirtualUser
    {
        [BsonId][BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Addresses { get; set; } = new List<string>();
        public DateTime CreationDate { get; set; }
    }
}
