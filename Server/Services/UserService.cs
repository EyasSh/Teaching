using Microsoft.AspNetCore.Mvc;
using Server.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;


namespace Server.Services
{
    interface IUserService
    {
        Task<User?> GetUserByEmail(string email);
        Task<User?> CreateUser(User u);
    }
    public class UserService:ControllerBase, IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IOptions<DBSettings> settings)
        {
            var userClient = new MongoClient(settings.Value.ConnectionString);
            var Database = userClient.GetDatabase(settings.Value.DatabaseName);
            _usersCollection = Database.GetCollection<User>(settings.Value.UsersCollectionName);
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            //use appropriate checks for password and email validation
           var filter = Builders<User>.Filter.Eq(u => u.Email,email);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }
       
       public async Task<User?> CreateUser(User user)
        {
            // Before creating, check if the user already exists by email
            var existingUser = await GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                return null;
            }
           user.Password= HashPassword(user.Password);
            await _usersCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User?> Login(string email,string password)
        {
            var filter = Builders<User>.Filter.Eq(u=>u.Email, email);
            var user = await _usersCollection.Find(filter).FirstOrDefaultAsync();
            if (user != null)
            {
                bool isVerified = VerifyPassword(password,user.Password);
                if (!isVerified)
                {
                    Console.WriteLine("Password is not verified returning null");
                    return null;
                }
                else
                {
                    return user;
                }
            }
            else
            {
                Console.WriteLine("User was not found");
                return null;
            }
            
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string plain,string hashed)
        {
           return BCrypt.Net.BCrypt.Verify(plain,hashed);
        }

    }
}
