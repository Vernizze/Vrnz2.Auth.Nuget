using System.Collections.Generic;

namespace Vrnz2.Auth.Nuget.DTOs
{
    public class AuthServerRequest
    {
        public string token { get; set; }
        public List<string> claims { get; set; }
    }
}
