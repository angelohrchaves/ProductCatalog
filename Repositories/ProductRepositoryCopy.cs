using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Repositories
{
    public class ProductRepositoryCopy
    {


        private readonly StoreDataContext _context;
        public ProductRepositoryCopy(StoreDataContext context)
        {
            _context = context;
        }


        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products
                .Include(i => i.Category)
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Category = x.Category.Title,
                    CategoryId = x.CategoryId
                })
                .AsNoTracking()
                .ToList();

        }


        public Product Get(int id)
        {
            return _context.Products.AsNoTracking().Where(w => w.Id == id).FirstOrDefault();
        }

        public void Post(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }


        public void Put(Product product)
        {

            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();

        }
                
        public void Delete(Product product)
        {            
            _context.Products.Remove(product);
            _context.SaveChanges();
                       
        }
    }
}
