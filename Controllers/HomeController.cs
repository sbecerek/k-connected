using Microsoft.AspNetCore.Mvc;
using k_connected.API.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace k_connected.API.Controllers
{
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly kconnectedDBContext ctx;

        public HomeController()
        {
            ctx = new kconnectedDBContext();
        }


        [HttpGet("otherusers")]
        public ActionResult getOtherUsers()
        {


            var q = ctx.Entity.Where(user => user.Username != HttpContext.User.Identity.Name).Include(k => k.Knowledge).ThenInclude(k => k.SkillNameNavigation).ToList();        

            return Ok(q);

        }


        [HttpGet("currentuser")]
        public ActionResult getCurrentUser()
        {


            var q = ctx.Entity.Where(user => user.Username == HttpContext.User.Identity.Name).Include(k => k.Knowledge).ThenInclude(k => k.SkillNameNavigation).ToList();

            return Ok(q);

        }

    }


}