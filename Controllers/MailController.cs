﻿using k_connected.API.Models;
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
        private void sendEmailViaWebApi(string to,string from,string tomail)
        {
            string subject = "Someone reached you on k-connected";
            string body = "Hello " + to + ", I checked your profile on k-connected reach me on the platform, " + from ;
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
        public ActionResult SendMail([FromBody] string to)
        {
            var from = CurrentUser.Username;
            Entity touser = ctx.Entity.Where(u => u.Username == to).FirstOrDefault();

            sendEmailViaWebApi(to, from, touser.Email );

            return Ok();
        }


    }
}
