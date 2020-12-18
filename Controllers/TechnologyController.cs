using Microsoft.AspNetCore.Mvc;
using k_connected.API.Data;
using k_connected.API.Models;
using System.Collections.Generic;

namespace k_connected.API.Controllers
{
    [Route("api/Technology")]
    public class TechnologyController : ControllerBase
    {
        
        public ActionResult MockData()
        {
            return Ok(UserData.Users);

        }


        [HttpPost]
        public ActionResult Filter([FromBody]string opt)
        {
            List<User> filtered = new List<User>();
            foreach (var item in UserData.Users)
            {
                if(item.Technologies.Contains(opt))
                    filtered.Add(item);
            }

            return Ok(filtered);
        }
    }
}