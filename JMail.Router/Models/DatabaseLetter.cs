using MongoDB.Bson.Serialization.Attributes;

namespace JMail.Relay.Models
{
    public class DatabaseLetter
    {
        [BsonId][BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string LetterEncrypted { get; set; }
    }

}
