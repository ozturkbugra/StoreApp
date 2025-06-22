using System.ComponentModel.DataAnnotations;

namespace StoreApp.Web.Models
{
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
    }


    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel>? Products { get; set; }
    }
}
