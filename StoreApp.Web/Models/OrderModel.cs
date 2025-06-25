using StoreApp.Data.Concrete;

namespace StoreApp.Web.Models
{
    public class OrderModel
    {
            public int Id { get; set; }

            public DateTime OrderDate { get; set; }

            public string Name { get; set; }
            public string City { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string AddressLine { get; set; }

            public Cart Cart { get; set; } = new();
        
    }
}
