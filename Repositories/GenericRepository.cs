using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StoreDataContext _context;
        private DbSet<T> _table;
        public GenericRepository(StoreDataContext _context)
        {
            this._context = _context;
            _table = _context.Set<T>();
        }
        
        public IEnumerable<T> Get()
        {
            _table = _context.Set<T>();
            return _table.AsNoTracking().ToList();
        }

        public T Get(object id)
        {
            _table = _context.Set<T>();
            return _table.Find(id);
        }

        public T Get(Func<T, bool> condition)
        {
            _table = _context.Set<T>();
            return _table.AsNoTracking().SingleOrDefault(condition);
        }
        
        public IEnumerable<T> GetFunction(Func<T, bool> condition)
        {
            _table = _context.Set<T>();
            return _table.AsNoTracking().Where(condition).ToList();

        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Update(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

        }               

        public void Delete(object id)
        {

            _table = _context.Set<T>();
            T existing = _table.Find(id);
            _table.Remove(existing);

        }
    }
}
