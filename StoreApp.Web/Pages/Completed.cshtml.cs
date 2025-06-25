using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreApp.Web.Pages
{
    public class CompletedModel : PageModel
    {
        public string OrderId { get; set; }
        public void OnGet(int OrderId)
        {
            ViewData["SiparisNo"] = OrderId;
        }
    }
}
