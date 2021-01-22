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

            var wholedb = ctx.Entities.Where(user => user.Username != HttpContext.User.Identity.Name).Include(k => k.Knowledges).ThenInclude(k => k.SkillNameNavigation).ToList();
            System.Console.WriteLine(opt);
            var filtered = new List<Entity>();
            filtered.Clear();

            foreach(string skill in opt)
            {

                foreach(Entity item in wholedb)
                {
                    foreach(Knowledge s in item.Knowledges)
                    {
                        //System.Console.WriteLine(s.SkillName.Length);
                        //System.Console.WriteLine(skill.Length);
                        if (s.SkillName.Equals(skill))
                            filtered.Add(item);
                    }

                
                }


            }

           

            return Ok(filtered);
        }
    }
}