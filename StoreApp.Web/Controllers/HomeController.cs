using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly IStoreRepository _repository;

    public HomeController(IStoreRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var products = _repository.Products.Select(p=> new ProductViewModel { 
        Id = p.Id,
        Category = p.Category,
        Description = p.Description,
        Name = p.Name,
        Price = p.Price
        }).ToList();

        return View(new ProductListViewModel
        {
            Products = products
        });
    }

  
}
