using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreApp.Data.Concrete;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Web.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string AddressLine { get; set; }

        [BindNever]
        public Cart? Cart { get; set; } = null!;

        public string CartName { get; set; }
        public string CartNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string Cvv { get; set; }



    }
}
