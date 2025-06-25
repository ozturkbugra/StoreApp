using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreApp.Data.Abstract;
using StoreApp.Web.Helpers;
using StoreApp.Web.Models;

namespace StoreApp.Web.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository _storeRepository;

        public CartModel(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public Cart? Cart { get; set; }
        public void OnGet()
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

        }

        public IActionResult OnPost(int Id)
        {
            var product = _storeRepository.Products.FirstOrDefault(x => x.Id == Id);

            if(product!= null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostRemove(int Id)
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            var product = _storeRepository.Products.FirstOrDefault(x => x.Id == Id);
            Cart.RemoveItem(product);
            HttpContext.Session.SetJson("cart", Cart);
            return RedirectToPage("/Cart");
        }
    }
}
