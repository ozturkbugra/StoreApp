using StoreApp.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDBContext _context;

        public EFOrderRepository(StoreDBContext context)
        {
            _context = context;
        }
        public IQueryable<Order> Orders => _context.Orders;

        public void SaveOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
