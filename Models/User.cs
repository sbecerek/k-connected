using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace k_connected.API.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Coordinate {get; set; }

        private List<string> _Technologies = new List<string>();

        public List<string> Technologies {
            get {return _Technologies;}

            set { _Technologies = value;}
        }
        

    }
}