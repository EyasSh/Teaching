using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace Server.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public  string Name { get; set; } = "John";
        [EmailAddress]
        [Required]
        public  string Email { get; set; } = "";
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-zA-Z]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters and contain at least one letter and one number")]
        public  string Password { get; set; } = "LobsterBeef997";
        [Required]
        public DateTime Birthday { get; private set; } // Make setter private

        [BsonIgnore]
        public string BirthdayString
        {   
            set
            {
                Console.WriteLine(value);
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        Console.WriteLine($"In the try of {nameof(BirthdayString)}");
                        DateTime d = DateTime.Parse(value, null, DateTimeStyles.RoundtripKind);
                        Console.WriteLine(d.ToString());
                        Birthday = DateTime.SpecifyKind(d, DateTimeKind.Utc); 
                        System.Console.WriteLine($"Birthday is :{d}");
                    }
                    catch
                    {
                        Console.WriteLine("An Error Occurred");
                    }
                }
            }
            get { return Birthday.ToString("o"); } // Return ISO 8601 string
        }
        
        public User()
        {
        }
        public User(string name, string email, string password, DateTime birthday)
        {
            Name = name;
            Email = email;  
            Password = password;
            Birthday = birthday;
        }
    }
}
