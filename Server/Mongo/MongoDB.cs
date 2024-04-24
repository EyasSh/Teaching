using MongoDB.Driver;
namespace Server.Mongo
{
    ///<summary> 
    ///a wrapper class encapsulating a MongoDB database with its collections
    ///this class is a singleton
    ///</summary>
    ///
    public class MongoDBWrapper
    {
        private readonly IMongoDatabase _db;
        public MongoDBWrapper(IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("MongoDBConnection");
            var dbName = configuration["DatabaseName"];

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dbName);
            //initialize collections here
            //Example
            // Collection1 = new MongoDbSet<YourDocumentModel1>(_db, "Collection1Name");
            //end collection init
        }
    }
}
