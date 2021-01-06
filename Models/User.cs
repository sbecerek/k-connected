using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace k_connected.API.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Passwd {get; set;}

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int Apartment { get; set; }
        public  string Email {get;set;}
        public List<Skill> Technologies { get; set; } = new List<Skill>();


    }
}