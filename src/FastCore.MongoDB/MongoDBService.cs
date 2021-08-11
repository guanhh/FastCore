using FastCore.Base;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FastCore.MongoDB
{
    public class MongoDBService : IMongoDBService
    {
        private readonly MongoDBOption _option;
        private readonly IMongoDatabase _database;
        public MongoDBService(IOptions<MongoDBOption> options)
        {
            _option = options.Value;

            var client = new MongoClient(_option.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(_option.Database);
            }
        }

        public async Task InsertOneAsync<T>(T t)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            await collection.InsertOneAsync(t);
        }

        public async Task InsertManyAsync<T>(List<T> list)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            await collection.InsertManyAsync(list);
        }

        public async Task DeleteManyAsync<T>(Expression<Func<T, bool>> conditions)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            await collection.DeleteManyAsync(conditions);
        }

        public async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> conditions)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            if (conditions != null)
            {
                return await collection.Find(conditions).ToListAsync();
            }
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task ReplaceOneAsync<T>(Expression<Func<T, bool>> conditions, T t)
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            await collection.ReplaceOneAsync(conditions, t);
        }


    }
}
