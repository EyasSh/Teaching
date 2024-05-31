using Microsoft.AspNetCore.Mvc;
using Server.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Server.Services
{
    interface IUserService
    {
        Task<User> GetUserByEmailandPass(string email,string password);
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
       
        public async Task<bool> CreateUser(User u)
        {
            
            var res= await GetUserByEmailandPass(u.Email, u.Password);
            bool newUserCreated = res==null;
            if (res == null)
            {
                u.Password = HashPassword(u.Password);
                User user = new User(u.Name, u.Email, u.Password,u.Birthday);
                _usersCollection.InsertOne(user);
                
            }
            return newUserCreated;

        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}
