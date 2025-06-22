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
        return View();
    }

  
}
