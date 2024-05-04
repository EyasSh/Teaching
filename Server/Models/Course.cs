using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Server.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("name")]
        [Required]
        public string courseName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price {  get; set; }
        
    }
}
