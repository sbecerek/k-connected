using Microsoft.AspNetCore.Mvc;
using k_connected.API.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace k_connected.API.Controllers
{
    [Route("api/Technology")]
    public class TechnologyController : ControllerBase
    {


        private readonly kconnectedDBContext ctx;

        public TechnologyController()
        {
            ctx = new kconnectedDBContext();
        }

        public ActionResult MockData()
        {
            return Ok();

        }


        [HttpPost]
        public ActionResult Filter([FromBody]string[] opt)
        {

            var wholedb = ctx.Entity.Where(user => user.Username != HttpContext.User.Identity.Name).Include(k => k.Knowledge).ThenInclude(k => k.SkillNameNavigation).ToList();
            System.Console.WriteLine(opt);
            var filtered = new List<Entity>();
            filtered.Clear();

            foreach(string skill in opt)
            {

                foreach(var item in wholedb)
                {
                    foreach(var s in item.Knowledge)
                    {
                        if (s.SkillName == skill)
                            filtered.Add(item);
                    }

                
                }


            }


            return Ok(filtered);
        }
    }
}