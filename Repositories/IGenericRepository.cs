using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> Get();
        T Get(object id);
        T Get(Func<T, bool> condition);
        IEnumerable<T> GetFunction(Func<T, bool> condition);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
    }
}
