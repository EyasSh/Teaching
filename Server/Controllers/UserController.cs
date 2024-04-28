using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using  Newtonsoft.Json.Linq;
using MongoDB.Entities;
using BCrypt;
using Server.Models;
using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Server.Mongo;

namespace Server.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
       private readonly MongoDBWrapper _wrapper;
       private readonly IMongoCollection<User> _userCollection;
        public UserController(MongoDBWrapper wrapper)
        {
            // Get the User collection from the database
            this._wrapper = wrapper;
            _userCollection = wrapper.Users;
        }
        // GET: UserController
        [HttpGet]
        [Route("/")]
        [AllowAnonymous]
        public IActionResult Index([FromBody] UserLoginModel model)
        {
            // If user credentials are provided, attempt authentication
            if (!string.IsNullOrEmpty(model?.Email) && !string.IsNullOrEmpty(model?.Password))
            {
                // Retrieve the user by email from the database
                var user = _userCollection.Find(u => u.Email == model.Email).SingleOrDefault();
                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    // Generate JWT token
                    var token = GenerateJwtToken(model.Email);
                    // Return user and token in the response
                    return Ok(new { User = user, Token = token });
                }
                else
                {
                    return Unauthorized(); // Invalid credentials
                }
            }
            else
            {
                return BadRequest(); // Bad request if credentials are not provided
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult PrivateData()
        {
            // Private data accessible only with authentication
            return Ok("Private data accessible only with authentication.");
        }

        private  bool IsValidUser(string email, string password)
        {
            // Your actual user validation logic goes here
            // For demonstration purposes, let's just return true for any non-empty email and password
            return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
        }

        private string GenerateJwtToken(string email)
        {
            var payload = new JwtPayload{
            { "email", email },
            { "exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds() }
            };

            var secret = Encoding.UTF8.GetBytes("ZdCfO1CpZMcSVbNl6TDe");
            var algorithm = new HMACSHA256Algorithm();

            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();

            var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("/signup")]
        public async Task<IActionResult> Create([FromBody] JObject formData)
        {
            try
            {
                // Extract properties from form data
                string name = formData.Value<string>("name");
                string email = formData.Value<string>("email");
                string password = formData.Value<string>("password");
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                {
                    return BadRequest("name or password exist already");
                }

                // Check if the password meets the criteria
                if (!IsPasswordValid(password))
                {
                    return BadRequest("Password must be at least 8 characters long and contain letters and numbers.");
                }

                // Check if the email is unique
                bool userUnique = await IsEmailUniqueAsync(email);
                if (userUnique)
                {
                    // Create user object
                    var user = new User { Name = name, Email = email, Password = BCrypt.Net.BCrypt.HashPassword(password) };

                    // Insert the user document into the MongoDB collection
                    await _userCollection.InsertOneAsync(user);

                    return Ok(new { user });
                }
                else
                {
                    return BadRequest("Email is already in use.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during user creation
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // GET: UserController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private async  Task<bool> IsEmailUniqueAsync(string email)
        {
            // Create a filter to find users with the specified email
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);

            // Execute the find operation asynchronously using a cursor
            using (var cursor = await _userCollection.FindAsync(filter))
            {
                // Check if any document matches the filter
                return !await cursor.AnyAsync();
            }
        }
        private bool IsPasswordValid(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 8 && password.Any(char.IsLetter) && password.Any(char.IsDigit);
        }
    }


    public class UserLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
   

