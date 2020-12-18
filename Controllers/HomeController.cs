using Microsoft.AspNetCore.Mvc;
using k_connected.API.Data;
using k_connected.API.Models;
using System.Collections.Generic;

namespace k_connected.API.Controllers
{
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        
        public ActionResult Get()
        {

             User u0 = new User {Username = "Suheyb", Coordinate="52.217856542437424,21.02057254525368"};
             u0.Technologies.Add("C++");
            UserData.Users.Add(u0);
            User u1 = new User {Username = "Devel0per", Coordinate="52.21792323154015,21.015806606512058"};
             u1.Technologies.Add("C#");
            UserData.Users.Add(u1);


            return Ok(UserData.Users);

        }

    }


}