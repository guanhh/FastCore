using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.Security
{
    public class JwtOption
    {
        public string Issuer { get; set; } = "http://localhost";
        public string Audience { get; set; } = "http://localhost";
        public int Expires { get; set; } = 3600;
        public string SecurityKey { get; set; } = "fastcore@1234567";
        public int RefreshTokenExpires { get; set; }
    }
}
