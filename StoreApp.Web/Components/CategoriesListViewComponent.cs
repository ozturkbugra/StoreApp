using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components
{
    public class CategoriesListViewComponent:ViewComponent
    {
        private readonly IStoreRepository _storeRepository;

        public CategoriesListViewComponent(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_storeRepository.Categories.Select(x=> new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Url = x.Url
            }).ToList());
        }
    }
}
