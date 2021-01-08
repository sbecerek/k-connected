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

            var wholedb = ctx.Entity.Where(user => user.Username != CurrentUser.Username).Include(k => k.Knowledge).ThenInclude(k => k.SkillNameNavigation).ToList();
            System.Console.WriteLine(opt);
            var filtered = new List<Entity>();
            filtered.Clear();

            foreach(string skill in opt)
            {
               // var skilltrimmed = skill.Trim('"');
               // var l = ctx.Entity.FromSqlRaw($"SELECT * FROM Entity JOIN Knowledge ON Entity.Username = Knowledge.Username JOIN Skill ON Knowledge.Skill_name = Skill.Skill_name WHERE Entity.Username = (SELECT Entity.Username FROM Entity JOIN Knowledge ON Entity.Username = Knowledge.Username JOIN Skill ON Knowledge.Skill_name = Skill.Skill_name WHERE Skill.Skill_name = '{skilltrimmed}')").ToList();
                //var l = ctx.Entity.Include(u => u.Knowledge.Where(k => k.SkillName == skill)).Where(k => k.Knowledge.Count != 0).ToList();
                //var l = ctx.Entity.Include(u => u.Knowledge.Any(k => k.SkillName == skill)).Select(s=>s).ToList();
                //var l = ctx.Entity.Select(
                //    c => new Entity()
                //    {
                //        Knowledge = (ICollection<Knowledge>)c.Knowledge.Where(s => s.SkillName == skill),
                //        Username = c.Username,
                //    }.Where(u => u.Username != CurrentUser.Username && u.Username == c.Username).Include(k => k.Knowledge)
                //    ).ToList();


                //ctx.Dispose();

                //foreach(var ll in l)
                //{
                //    var q = ctx.Entity.;
                //    filtered = filtered.Concat(q).ToList();
                //}

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