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
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.Categories)
                .WithMany(x => x.Products)
                .UsingEntity<ProductCategory>(); // category ile product çoka çok oldu 

            modelBuilder.Entity<Category>() // url benzersiz oldu
                .HasIndex(x => x.Url)
                .IsUnique();

            modelBuilder.Entity<Product>().HasData(
                
                new List<Product>()
                {
                    new() {Id =1, Name = "Samsung S20 FE", Price = 10000, Description = "128 gb"},
                    new() {Id =2, Name = "Samsung S21 FE", Price = 20000, Description = "128 gb"},
                    new() {Id =3, Name = "Samsung S22 FE", Price = 25000, Description = "256 gb"},
                    new() {Id =4, Name = "Samsung S23 FE", Price = 30000, Description = "256 gb"},
                    new() {Id =5, Name = "Fiat Linea", Price = 35000, Description = "1.3 Multijet"},
                    new() {Id =6, Name = "Asus PC", Price = 40000, Description = "1000 gb"},
                    new() {Id =7, Name = "Buzdolabı", Price = 45000, Description = "güzel buzdolabı"},
                }

                );

            modelBuilder.Entity<Category>().HasData(

                new List<Category>()
                {
                    new() {Id =1, Name = "Telefon", Url="telefon"},
                    new() {Id =2, Name = "Araba", Url="araba"},
                    new() {Id =3, Name = "Bilgisayar", Url="bilgisayar"},
                    new() {Id =4, Name = "Beyaz Eşya", Url="beyaz-esya"}
                  
                }

              );

            modelBuilder.Entity<ProductCategory>().HasData(

                new List<ProductCategory>()
                {
                    new() {ProductId =1 , CategoryId=1},
                    new() {ProductId =2 , CategoryId=1},
                    new() {ProductId =3 , CategoryId=1},
                    new() {ProductId =4 , CategoryId=1},
                    new() {ProductId =5 , CategoryId=2},
                    new() {ProductId =6 , CategoryId=3},
                    new() {ProductId =7 , CategoryId=4},
                    new() {ProductId =1 , CategoryId=4},

                   
                }

              );
        }
    }
}
