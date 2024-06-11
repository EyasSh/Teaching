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
    public class UserService: IUserService
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
                throw new InvalidOperationException("User already exists with this email.");
            }

            await _usersCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User?> Login(string email,string password)
        {
            var filter = Builders<User>.Filter.Eq(u=>u.Email, email);
            var users = await _usersCollection.Find(filter).ToListAsync();
            if (users.Count >1|| users.Count == 0)
            {
                return null;
            }
            else
            {
                if(VerifyPassword(password,users[0].Password))
                {
                    return users[0];
                }
                else
                {
                    return null;
                }
            }
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string plain,string hashed)
        {
           return BCrypt.Net.BCrypt.EnhancedVerify(plain,hashed);
        }

    }
}
