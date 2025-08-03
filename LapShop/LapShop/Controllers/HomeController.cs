using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LapShop.Controllers
{
    public class HomeController : Controller
    {
        IItems oClsItems;
        ISliders oClsSliders;
        ICategories oClsCategories;
        public HomeController(IItems item, ISliders oSliders, ICategories categories)
        {
            oClsItems = item;
            this.oClsSliders = oSliders;
            this.oClsCategories = categories;
        }
        public IActionResult Index()
        {
            VmHomePage vm = new VmHomePage();
            vm.lstAllItems = oClsItems.GetAllItemsData(null).ToList();
            vm.lstRecommendedItems = oClsItems.GetAllItemsData(null).ToList();
            vm.lstNewItems = oClsItems.GetAllItemsData(null).ToList();
            vm.lstFreeDelivry = oClsItems.GetAllItemsData(null).ToList();
            vm.lstSliders = oClsSliders.GetAll();
            vm.lstCategories = oClsCategories.GetAll().ToList();
            return View(vm);
        }
        public IActionResult listitem()
        {
            
            return View();
        }
        [HttpGet]
        public List<TbCategory> getallite()
        {
            var item = oClsCategories.GetAll();
            return item;
        }

    }
}
