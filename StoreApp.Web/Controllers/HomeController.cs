using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

    public IActionResult Index(int page=1)
    {
        var products = _repository
            .Products
            .Skip((page - 1) * pageSize)  
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price
                }).Take(pageSize);


        return View(new ProductListViewModel
        {
            Products = products,
            PageInfo = new PageInfo
            {
                ItemsPerPage = pageSize,
                TotalItems = _repository.Products.Count(),
                CurrentPage= page
            }
        });
    }

  
}
