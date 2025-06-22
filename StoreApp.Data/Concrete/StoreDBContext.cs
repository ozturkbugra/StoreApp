using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class StoreDBContext: DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options):base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
