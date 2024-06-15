using System;
using System.Collections.Generic;
namespace Server.DTO
{
    public class UserDTO
    {
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? BirthdayString { get; set; }
    }
}
