using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data.Concrete
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Url { get; set; }

        public List<Product> Products { get; set; } = new();

    }
}
