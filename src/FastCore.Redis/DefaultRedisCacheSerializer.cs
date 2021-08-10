using StackExchange.Redis;
using System.Text.Json;

namespace FastCore.Redis
{
    public class DefaultRedisCacheSerializer : IRedisCacheSerializer
    {
        public DefaultRedisCacheSerializer() { }

        public T Deserialize<T>(RedisValue objbyte)
        {
            return JsonSerializer.Deserialize<T>(objbyte);
        }

        public object Deserialize(RedisValue objbyte)
        {
            return JsonSerializer.Deserialize(objbyte, typeof(object));
        }

        public string Serialize(object value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}
