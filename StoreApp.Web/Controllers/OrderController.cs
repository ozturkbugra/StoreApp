using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers
{
    public class OrderController : Controller
    {
        
        private Cart _cart;
        private IOrderRepository _orderRepository;
        public OrderController(Cart cart, IOrderRepository orderRepository)
        {
            _cart = cart;
            _orderRepository = orderRepository;
        }

        public IActionResult CheckOut()
        {
            return View(new OrderModel()
            {
                Cart = _cart
            });
        }

        [HttpPost]
        public IActionResult CheckOut(OrderModel model)
        {
            if(_cart.Items.Count == 0)
            {
                ModelState.AddModelError("", "Sepetinizde Ürün Yok");
            }

            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Name = model.Name,
                    Email = model.Email,
                    City = model.City,
                    Phone = model.Phone,
                    AddressLine = model.AddressLine,
                    OrderDate = DateTime.Now,
                    OrderItems = _cart.Items.Select(x => new OrderItem
                    {
                        ProductId = x.Product.Id,
                        Price = (double)x.Product.Price,
                        Quantity = x.Quantity
                    }).ToList()
                };
                _orderRepository.SaveOrder(order);
                _cart.Clear();
                return RedirectToPage("/Completed", new {OrderId= order.Id});
            }
            else
            {
                model.Cart = _cart;
                return View(model);
            }
               
                
        }


    }
}
