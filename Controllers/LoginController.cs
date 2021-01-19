using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace k_connected.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public LoginController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        // GET: api/<NameController>
        [HttpGet]
        public ActionResult Get()
        {
            // Console.WriteLine();
            //CurrentUser.Username = HttpContext.User.Identity.Name;


            return Ok(HttpContext.User.Identity.Name);
        }

        // GET api/<NameController>/5

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromForm] LoginCredentials user) 
        {
            var token = jwtAuthenticationManager.Authenticate(user.Username, user.Password);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

    }
}
