﻿using System;
using System.Collections.Generic;

namespace k_connected.API.Models
{
    public partial class Entity
    {
        public Entity()
        {
            Knowledge = new HashSet<Knowledge>();
        }

        public string Username { get; set; }
        public string Passwd { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Apartment { get; set; }

        public virtual ICollection<Knowledge> Knowledge { get; set; }
    }
}