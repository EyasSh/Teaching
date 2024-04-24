using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Entities;
using Server.Models;

namespace Server.Controllers
{
    public class UserController : Controller
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserController(IMongoDatabase database)
        {
            // Get the User collection from the database
            _userCollection = database.GetCollection<User>("User");
        }
        // GET: UserController
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            //implement token logic before returning a response

            return View() ;
        }

        // GET: UserController/Details/5
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password")]User user)
        {
            try
            {
                if (!IsPasswordValid(user.Password))
                {
                    return BadRequest(("Password must be at least 8 characters long and contain letters and numbers.").ToLower());
                }
                bool userUnique = await IsEmailUniqueAsync(user.Email);
                if (userUnique)
                {
                  await _userCollection.InsertOneAsync(user);
                  return Created();
                }
                else
                {
                    return BadRequest("email already in use");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
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
        private async Task<bool> IsEmailUniqueAsync(string email)
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
}
