using Microsoft.AspNetCore.Mvc;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers
{
    public class OrderController : Controller
    {

        private Cart _cart;

        public OrderController(Cart cart)
        {
            _cart = cart;
        }

        public IActionResult CheckOut()
        {
            return View(new OrderModel()
            {
                Cart = _cart
            });
        }
    }
}
