using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace FastCore.Abstract.Security
{
    public class StringConstants
    {
        public static readonly string UserId = ClaimTypes.NameIdentifier;
        public static readonly string UserName = ClaimTypes.Name;
        public static readonly string Role = ClaimTypes.Role;
        public static readonly string RefreshToken = "FastCore:RefreshToken";

        //其它

    }
}
