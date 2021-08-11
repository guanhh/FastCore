using System;
using System.Collections.Generic;
using System.Text;

namespace FastCore.Abstract.Security
{
    public class TokenResp
    {
        public string accesstoken { get; set; }

        public string refreshtoken { get; set; }
    }
}
