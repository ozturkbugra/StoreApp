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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                
                new List<Product>()
                {
                    new() {Id =1, Name = "Samsung S20 FE", Price = 10000, Description = "128 gb", Category="Telefon"},
                    new() {Id =2, Name = "Samsung S21 FE", Price = 20000, Description = "128 gb", Category="Telefon"},
                    new() {Id =3, Name = "Samsung S22 FE", Price = 25000, Description = "256 gb", Category="Telefon"},
                    new() {Id =4, Name = "Samsung S23 FE", Price = 30000, Description = "256 gb", Category="Telefon"},
                    new() {Id =5, Name = "Samsung S24 FE", Price = 35000, Description = "256 gb", Category="Telefon"},
                    new() {Id =6, Name = "Samsung S25 FE", Price = 40000, Description = "1000 gb", Category="Telefon"},
                    new() {Id =7, Name = "Samsung S26 FE", Price = 45000, Description = "1000 gb", Category="Telefon"},
                }

                );
            
           
        }
    }
}
