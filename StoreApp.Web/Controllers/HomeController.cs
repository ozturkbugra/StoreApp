using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly IStoreRepository _repository;
    private readonly IMapper _mapper;

    public int pageSize = 3;

    public HomeController(IStoreRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public IActionResult Index(string category,int page=1)
    {
        ViewBag.SelectedCategory= RouteData?.Values["category"];


        return View(new ProductListViewModel
        {
            Products = _repository.GetProductsByCategory(category,page,pageSize)
            .Select(x=>_mapper.Map<ProductViewModel>(x)),
            PageInfo = new PageInfo
            {
                ItemsPerPage = pageSize,
                TotalItems = _repository.GetProductCount(category),
                CurrentPage= page
            }
        });
    }

  
}
