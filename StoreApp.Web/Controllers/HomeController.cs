using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly IStoreRepository _repository;

    public int pageSize = 3;

    public HomeController(IStoreRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index(string category,int page=1)
    {
        ViewBag.SelectedCategory= RouteData?.Values["category"];


        return View(new ProductListViewModel
        {
            Products = _repository.GetProductsByCategory(category,page,pageSize).Select(p => new ProductViewModel
            {
                Id = p.Id,
                Description = p.Description,
                Name = p.Name,
                Price = p.Price
            }),
            PageInfo = new PageInfo
            {
                ItemsPerPage = pageSize,
                TotalItems = _repository.GetProductCount(category),
                CurrentPage= page
            }
        });
    }

  
}
