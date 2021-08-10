using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastCore.Redis
{
    public interface IRedisCacheSerializer
    {

        T Deserialize<T>(RedisValue objbyte);

        /// <summary>
        ///     Creates an instance of the object from its serialized string representation.
        /// </summary>
        /// <param name="objbyte">String representation of the object from the Redis server.</param>
        /// <returns>Returns a newly constructed object.</returns>
        /// <seealso cref="Serialize" />
        object Deserialize(RedisValue objbyte);

        /// <summary>
        ///     Produce a string representation of the supplied object.
        /// </summary>
        /// <param name="value">Instance to serialize.</param>
        /// <returns>Returns a string representing the object instance that can be placed into the Redis cache.</returns>
        /// <seealso cref="Deserialize" />
        string Serialize(object value);
    }
}
