using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System.ComponentModel.DataAnnotations;
namespace Server.Models
{
    public class User:Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; init; }
        [DataType(DataType.EmailAddress)]
        public string Name { get; set; }
        public string Email { get; init; }
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain letters and numbers")]
        public string Password { get; set; }
    }
}
