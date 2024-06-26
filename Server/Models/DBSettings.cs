﻿namespace Server.Models
{
    public class DBSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string UsersCollectionName { get; set; } = null;
        public string CoursesCollectionName { get; set; } = null;
        public string UserCourseCollectionName { get; set; }=null;
    }
}
