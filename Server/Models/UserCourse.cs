using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Server.Models
{
    public class UserCourse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
      
        public string UserId { get; set; } // Reference to User Id
      
        public string CourseId { get; set; } // Reference to Course Id

        // Additional properties like enrollment date, progress, etc.
    }

}
