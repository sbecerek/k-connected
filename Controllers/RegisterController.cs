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
    public class RegisterController : Controller
    {
       


        // POST: RegisterController/Create
        [HttpPost]
        public ActionResult Register(/*[FromForm]Entity entity*/[FromForm] string username, [FromForm] string email, [FromForm] string bio, [FromForm] string[] technologyselect, [FromForm] string street, [FromForm] int apartment, [FromForm] string city, [FromForm] string country, [FromForm] string password, [FromForm] string firstname, [FromForm] string lastname)
        {
            Entity objEmp = new Entity();
            using (kconnectedDBContext objDataContext = new kconnectedDBContext())
            {
                
                // fields to be insert
                objEmp.Username = username;
                objEmp.Firstname = firstname;
                objEmp.Lastname = lastname;
                objEmp.Email = email;
                objEmp.Street = street;
                objEmp.City = city;
                objEmp.Country = country;
                objEmp.Apartment = apartment;
                objEmp.Passwd = password;
                objDataContext.Add(objEmp);
                objDataContext.SaveChanges();
                foreach (var t in technologyselect)
                {
                    string l = objDataContext.Skills.Where(x => x.SkillName == t).Select(x=>x.SkillName).FirstOrDefault();
                    Knowledge k = new Knowledge();
                    k.SkillName = l;
                    k.Username = username;
                    objDataContext.Knowledges.Add(k);
                    objDataContext.SaveChanges();
                }
                

                // executes the commands to implement the changes to the database
            }



            return Ok(objEmp);
        }

       
    }
}
