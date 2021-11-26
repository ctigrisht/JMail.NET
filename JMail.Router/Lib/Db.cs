global using MongoDB.Driver;

namespace JMail.Relay.Lib
{
    public class Db
    {
        public static MongoClient Client;
        public static void NewClient(string connectionString) => Client = new MongoClient(connectionString);
    }
}
