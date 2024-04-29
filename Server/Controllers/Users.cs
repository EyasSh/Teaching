using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.Models;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/users")]
    //Summary:
    //  some of the problems encountered
    //  inherit from controllerbase not controller and return HTTP responses
    //  set routes correctly as well
    //
    public class Users : ControllerBase
    {
        private readonly UserService _userService;
        public Users(UserService userService)
        {
            _userService = userService;
        }
        [Route("/")]
        [HttpGet]
        // GET: Users
        public async  Task<IActionResult> Index()
        {
            //await db or other method calls here
            return Ok();
        }
        [Route("/Home")]
        [HttpGet]
        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return Ok();
        }

        // GET: Users/Create
        [Route("/signup")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(User user)
        {
            var isCreated = await _userService.CreateUser(user.Name,user.Email,user.Password); 
            if(isCreated)
                return Ok(user);
            else
                return BadRequest();
        }

        // POST: Users/Create
        [Route("/signupForm")]
        [HttpPost]
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
        [Route("editUser/{id}")]
        [HttpPut]
        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return Ok();
        }

        // POST: Users/Edit/5
        [Route("editForm")]
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
                return BadRequest();
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
