using MongoDB.Driver;
using MongoDB.Entities;
using Server.Models;
namespace Server.Mongo
{
    ///<summary> 
    ///a wrapper class encapsulating a MongoDB database with its collections
    ///this class is a singleton
    ///</summary>
    ///
    public class MongoDBWrapper:DBContext
    {
        private readonly IMongoDatabase _db;
        public IMongoCollection<User> Users { get; private set; }
        public IMongoCollection<Courses> Courses { get; private set; }
        
        public MongoDBWrapper(IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("MongoDBConnection");
            var dbName = "TeachingSite";

            var client = new MongoClient(connectionString);
             _db = client.GetDatabase(dbName);
            //initialize collections here
            //Example
            Users = _db.GetCollection<User>("Users");
            Courses = _db.GetCollection<Courses>("Courses");
            //end collection init
        }
    }
}
