using System.ComponentModel.DataAnnotations;

namespace StoreApp.Web.Models
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}
