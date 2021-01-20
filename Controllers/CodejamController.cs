using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using k_connected.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace k_connected.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodejamController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            using (kconnectedDBContext objDataContext = new kconnectedDBContext())
            {
                var q = objDataContext.CodeJams.ToList();
                return Ok(q);
            }
        }


        // POST: RegisterController/Create
        [HttpPost("register")]
        public ActionResult Register([FromForm] string title, [FromForm] string description)
        {
            CodeJam codeJam = new CodeJam();

            using (kconnectedDBContext objDataContext = new kconnectedDBContext())
            {
                codeJam.Host = HttpContext.User.Identity.Name;
                codeJam.Title = title;
                codeJam.Descp = description;
                objDataContext.Add(codeJam);
                objDataContext.SaveChanges();
                
            }

                return Ok(codeJam);
        }


    }
}
