using System.Runtime.InteropServices;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.Models;

namespace Server.Services
{
    public interface ICourseService
    {

    }
    public class CourseService:ICourseService
    {
      readonly IMongoCollection<Course> _courses;
      readonly IMongoCollection<UserCourse> _usercourse;
        public CourseService (IOptions<DBSettings> settings)
        {
            var userClient = new MongoClient(settings.Value.ConnectionString);
            var Database = userClient.GetDatabase(settings.Value.DatabaseName);
            _courses = Database.GetCollection<Course>(settings.Value.CoursesCollectionName);
            
            try{
                _usercourse = Database.GetCollection<UserCourse>(settings.Value.UserCourseCollectionName);
            }
            catch (Exception ex)
            {
                Console.WriteLine ("Fail here");
            }
            
        }
        /// <summary>
        /// Gets all courses in the collection
        /// </summary>
        /// <returns>a nullable list of courses</returns>
        public async Task<List<Course>?> GetCourses()
        {

            var allCourses = await _courses.Find(_ => true).Project(_ => _).ToListAsync();

            return allCourses;

            
        }
        /// <summary>
        /// subcribes a user to a course by ID
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task SubscribeUserToCourseAsync(string courseId, string userId)
        {
            // Check if user is already subscribed (optional)
            // You can uncomment this block to prevent duplicate subscriptions
            // var isAlreadySubscribed = await IsUserSubscribedAsync(courseId, userId);
            // if (isAlreadySubscribed)
            // {
            //     return; // User already subscribed, handle appropriately
            // }

            // Add new user course document
            await _usercourse.InsertOneAsync(new UserCourse { UserId = userId, CourseId = courseId });
        }
        /// <summary>
        /// checks if a user is subscribed to a course
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="userId"></param>
        /// <returns>true if a user is subscribed false if not</returns>
        private async Task<bool> IsUserSubscribedAsync(string courseId, string userId)
        {
            var filter = Builders<UserCourse>.Filter.And(
                Builders<UserCourse>.Filter.Eq(uc => uc.UserId, userId),
                Builders<UserCourse>.Filter.Eq(uc => uc.CourseId, courseId));
            return await _usercourse.Find(filter).AnyAsync(); // Check if any documents exist
        }
    }
}
