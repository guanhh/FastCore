using System;
using System.Collections.Generic;
using System.Text;

namespace FastCore.Redis
{
    public class RedisCacheOptions
    {
        public string ConnectionString { get; set; } = "127.0.0.1:6379";

        public int DatabaseId { get; set; } = 0;

    }
}
