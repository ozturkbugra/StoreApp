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

        public void CreateProduct(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
