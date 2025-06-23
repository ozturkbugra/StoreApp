using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Abstract
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDBContext _context;

        public EFStoreRepository(StoreDBContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
        public IQueryable<Category> Categories => _context.Categories;

        public void CreateProduct(Product entity)
        {
            throw new NotImplementedException();
        }

        public int GetProductCount(string category)
        {
            return category == null
                  ? Products.Count()
                  : Products.Include(x => x.Categories).Where(x => x.Categories.Any(c => c.Url == category)).Count();
        }

        public IEnumerable<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            var products = Products;

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Include(x => x.Categories).Where(x => x.Categories.Any(c => c.Url == category));
            }

            return products.Skip((page - 1) * pageSize).Take(pageSize);

        }
    }
}
