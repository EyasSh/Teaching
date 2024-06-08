using Microsoft.AspNetCore.Mvc;
using Server.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Server.Services
{
    interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> CreateUser(User u);
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
        public async Task<User> GetUserByEmail(string email)
        {
            //use appropriate checks for password and email validation
           var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }
       
        public async Task<bool> CreateUser(User u)
        {
            
            // Check if a user with the same email already exists
             var existingUser = await GetUserByEmail(u.Email);
    
             // If an existing user with the same email is found, return false
            if (existingUser != null)
            {
                return false;
            }

            // If no existing user is found, proceed to create the new user
            u.Password = HashPassword(u.Password);
            _usersCollection.InsertOne(u);
            return true;
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}
