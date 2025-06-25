using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
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
            // 🔥 Cart'ı en başta atıyoruz
            model.Cart = _cart;

            if (_cart.Items.Count == 0)
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
                    OrderItems = _cart.Items.Select(x => new StoreApp.Data.Concrete.OrderItem
                    {
                        ProductId = x.Product.Id,
                        Price = (double)x.Product.Price,
                        Quantity = x.Quantity
                    }).ToList()
                };

                var payment = ProcessPayment(model); // artık model.Cart null değil

                if (payment != null && payment.Status == "success")
                {
                    _orderRepository.SaveOrder(order);
                    _cart.Clear();
                    return RedirectToPage("/Completed", new { OrderId = order.Id });
                }

                ModelState.AddModelError("", "Ödeme başarısız: " + (payment?.ErrorMessage ?? "Bilinmeyen hata"));
                return View(model);
            }
            else
            {
                return View(model); // model.Cart zaten yukarıda atanmıştı
            }
        }


        private Payment ProcessPayment(OrderModel model)
        {
            Options options = new Options
            {
                ApiKey = "sandbox-OI3w2AUULDy6EL3kW5eaQiWP5x7q7iEb",
                SecretKey = "sandbox-LJECfTkYGsxFtPBa3eqd8EPc0E6SKMKr",
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };

            var total = model.Cart.CalculateTotal();
            var totalStr = total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

            CreatePaymentRequest request = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = new Random().Next(111111111, 999999999).ToString(),
                Price = total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                PaidPrice = total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "B67832",
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString()
            };

            request.PaymentCard = new PaymentCard
            {
                CardHolderName = model.CartName,
                CardNumber = model.CartNumber,
                ExpireMonth = model.ExpirationMonth,
                ExpireYear = model.ExpirationYear,
                Cvc = model.Cvv,
                RegisterCard = 0
            };

            request.Buyer = new Buyer
            {
                Id = "BY789",
                Name = "Buğra",
                Surname = "Öztürk",
                GsmNumber = "+905350000000",
                Email = "email@email.com",
                IdentityNumber = "74300864791",
                LastLoginDate = "2015-10-05 12:43:35",
                RegistrationDate = "2013-04-21 15:12:09",
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732"
            };

            request.ShippingAddress = new Address
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };

            request.BillingAddress = new Address
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742"
            };

            request.BasketItems = new List<BasketItem>();
            foreach (var item in model.Cart.Items)
            {
                request.BasketItems.Add(new BasketItem
                {
                    Id = item.Product.Id.ToString(),
                    Name = item.Product.Name,
                    Category1 = item.Product.Name ?? "Genel",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = Convert.ToDouble(item.Product.Price).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
                });
            }

            try
            {
                var payment = Payment.Create(request, options).Result;
                return payment;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ödeme işlemi sırasında hata oluştu: " + ex.Message);
                return null;
            }
        }


    }
}
