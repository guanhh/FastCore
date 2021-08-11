using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.Base
{
    public interface IMongoDBService
    {
        Task InsertOneAsync<T>(T t);

        Task InsertManyAsync<T>(List<T> list);

        Task DeleteManyAsync<T>(Expression<Func<T, bool>> conditions);

        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> conditions);

        Task ReplaceOneAsync<T>(Expression<Func<T, bool>> conditions, T t);
    }
}
