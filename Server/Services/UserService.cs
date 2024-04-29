using Microsoft.AspNetCore.Mvc;
using Server.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;


namespace Server.Services
{
    interface IUserService
    {
        Task<User> GetUserByEmailandPass(string email,string password);
        Task<bool> CreateUser(string name, string email, string password);
    }
    public class UserService: IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IOptions<DBSettings> settings)
        {
            var userClient = new MongoClient(settings.Value.ConnectionString);
            var Database = userClient.GetDatabase(settings.Value.DatabaseName);
            _usersCollection = Database.GetCollection<User>(settings.Value.UsersCollectionName);
        }
        public async Task<User> GetUserByEmailandPass(string email, string password)
        {
            //use appropriate checks for password and email validation
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(u => u.Email, email),
                Builders<User>.Filter.Eq(u => u.Password, HashPassword(password)) // Hash password
                   );

            // Find the first user matching the filter
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
            

        }
        public async Task<bool> CreateUser(string name, string email, string password)
        {
            bool isCreated = false;
            var res= await GetUserByEmailandPass(email, password);
            if (res!=null)
            {
                return isCreated;
            }
            else
            {
                password= HashPassword(password);
                User user = new User(name,email,password);
                _usersCollection.InsertOne(user);
                isCreated = true;
                return isCreated;
            }
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}
