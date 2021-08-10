using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCore.Redis
{
    public interface IRedisCacheDatabaseProvider
    {
        IDatabase GetDatabase();
    }
}
