using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace k_connected
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}
