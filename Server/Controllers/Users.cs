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
        
        [HttpGet]
        // GET: Users
        public async  Task<IActionResult> Login([Bind("Id,Email,Password")] User u)
        {
            //await db or other method calls here
           User res =   await _userService.GetUserByEmailandPass(u.Email,u.Password);
            if (res!=null)
            {
                var token = GenerateJwtToken(res);
                return Ok(new{token});
            }
            return NotFound();
            
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
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Birthday")]User user)
        {
            var res = await _userService.CreateUser(user); 
            if(res)
            {
                return Ok();
            }
            return Conflict();
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
