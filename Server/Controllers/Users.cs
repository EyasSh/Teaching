using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MongoDB.Bson;
using Server.DTO;

namespace Server.Controllers
{

    /// <summary>
    /// some of the problems encountered
    ///  inherit from controllerbase not controller and return HTTP responses
    ///  set routes correctly as well
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class Users : ControllerBase
    {
        private readonly UserService _userService;
        private readonly CourseService _courseService;
        private readonly IConfiguration _configuration;
        public Users(UserService userService,CourseService courseService, IConfiguration configuration)
        {
            _userService = userService;
            _courseService = courseService;
            _configuration = configuration;
        }
        
        [HttpPost]
        // GET: Users
    public async Task<IActionResult> Login([FromBody] UserDTO request)
    {
        System.Console.WriteLine($"{request.Email}\n{request.Password}");
        if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            Console.WriteLine("In null check");
            return BadRequest(new { Message = "Email and password are required." });
        }

        var userLogging = await _userService.Login(request.Email, request.Password);
        if (userLogging == null)
        {
            Console.WriteLine("in userLogging null check");
            return BadRequest(new { Message = "Invalid email or password. or user does not exist" });
        }

        var token = GenerateJwtToken(userLogging);
        HttpContext.Response.Headers["Authorization"] = $"Bearer {token}";

        return Ok(new { User = userLogging });
    }



        
        [HttpGet("Home")]
        // GET: Users/Details/5
        public IActionResult Home(ObjectId id)
        {
            return Ok();
        }

        // GET: Users/Create
        
        [HttpPost("signup")]
        [AllowAnonymous]
        //TODO: Fix the user birthday setter (Done)
        
    public async Task<IActionResult> Create([FromBody] UserDTO userDto)
    {
        try
        {
            // Validate required fields
            if (userDto == null || string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password) || string.IsNullOrEmpty(userDto.BirthdayString))
            {
                return BadRequest("Name, Email, Password, and BirthdayString are required.");
            }

            // Parse birthday string to DateTime (example assumes format "yyyy-MM-dd")
            

            // Create a User object to pass to the service layer
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                BirthdayString = userDto.BirthdayString // Assuming User model has a DateTime property for Birthday
            };

            // Call the user service to create the user
            var result = await _userService.CreateUser(user);

            if (result != null)
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }
        catch (JsonException e)
        {
            return BadRequest(new { Message = $"Invalid data provided in JSON\nMessage: {e.Message}\nStack Trace\n{e.StackTrace}" });
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = $"An exception occurred with the below message\n{e.Message}\nStack Trace:\n{e.StackTrace}" });
        }
    }

        // POST: Users/Create
       
        [HttpPost("signupForm")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpPut("editUser")]
        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return Ok();
        }

        // POST: Users/Edit/5
      
        [HttpPost("editForm")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return BadRequest();
            }
        }
        private string GenerateJwtToken(User user)
        {
            if (user == null)
            {
                return "";
            }
            else
            {
                 var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    
                    // Add more claims if needed
                }),
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtSettings:ExpirationDays"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            }
           
        }
        //// GET: Users/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Users/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
