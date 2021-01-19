using k_connected.API.Models;
using k_connected.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace k_connected.Controllers
{
    [Route("api/mail")]
    public class MailController : Controller
    {

        private readonly kconnectedDBContext ctx;

        public MailController()
        {
            ctx = new kconnectedDBContext();
        }
        private void sendEmailViaWebApi(string to,string from,string tomail, string text)
        {
            string subject = "Someone reached you on k-connected";
            string body =  text + "\n" + from ;
            string FromMail = "mailkconnected@gmail.com";
            string emailTo = tomail;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            SmtpServer.Port = 25; 
            SmtpServer.Credentials = new System.Net.NetworkCredential("mailkconnected@gmail.com", "Ab.123456789");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }


        [HttpPost("sendmail")]
        public ActionResult SendMail([FromBody] Message message)
        {
            var from = HttpContext.User.Identity.Name;
            Entity touser = ctx.Entity.Where(u => u.Username == message.Username).FirstOrDefault();

            sendEmailViaWebApi(message.Username, from, touser.Email,message.Text );

            return Ok();
        }


    }
}
