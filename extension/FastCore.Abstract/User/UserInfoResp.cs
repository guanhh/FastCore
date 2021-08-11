using System;
using System.Collections.Generic;
using System.Text;

namespace FastCore.Abstract
{
    public class UserInfoResp
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int Status { get; set; }
    }
}
