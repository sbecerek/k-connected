using System;
using System.Collections.Generic;

#nullable disable

namespace k_connected.API.Models
{
    public partial class CodeJam
    {
        public int CodejamId { get; set; }
        public string Host { get; set; }
        public string Descp { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int? Apartment { get; set; }

        public virtual Entity HostNavigation { get; set; }
    }
}
