using System.Collections.Generic;
using Vrnz2.BaseContracts.DTOs.Base;

namespace Vrnz2.Auth.Nuget.DTOs
{
    public class AuthServerRequest
        : BaseDTO.Request
    {
        public string token { get; set; }
        public List<string> claims { get; set; }
    }
}
