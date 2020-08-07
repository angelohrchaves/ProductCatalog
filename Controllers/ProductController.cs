using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModel;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreDataContext _context;
        public ProductController(StoreDataContext context)
        {
            _context = context;
        }
        
        [Route("v1/products")]
        [HttpGet]
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
                     CategoryId = x.CategoryId,
                     Image = x.Image
                 })
                 .AsNoTracking()
                 .ToList();

        }

        [Route("v1/products/test")]
        [HttpGet]
        public ActionResult<string> GetString()
        {
            return "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like)." +
                "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, Lorem ipsum dolor sit amet.., comes from a line in section 1.10.32";
                

        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public Product Get(int id)
        {
            return _context.Products.AsNoTracking().Where(w => w.Id == id).FirstOrDefault();
        }

        [Route("v1/products/")]
        [HttpPost]
        public ResultViewModel Post([FromBody] EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Falha ao cadastrar um produto",
                    Data = model.Notifications
                };
            }
            Product product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                Image = model.Image,
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                CategoryId = model.CategoryId
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return new ResultViewModel
            {
                Success = true,
                Message = "Produto Cadastrado com sucesso!",
                Data = product
            };
        }

        [Route("v1/products/")]
        [HttpPut]
        public ResultViewModel Put([FromBody] EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Falha ao editar um produto",
                    Data = model.Notifications
                };
            }
            Product product = _context.Products.Find(model.Id);

            product.Title = model.Title;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.Image = model.Image;
            product.CreateDate = DateTime.Now;
            product.LastUpdateDate = DateTime.Now;
            product.CategoryId = model.CategoryId;

            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = product
            };
        }

        [Route("v1/products/")]
        [HttpDelete]
        public ResultViewModel Delete([FromBody] EditorProductViewModel model)
        {
            //Product product = ;
            _context.Products.Remove(_context.Products.Find(model.Id));
            _context.SaveChanges();

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto deletado com sucesso!",
                Data = null
            };
        }
    }
}