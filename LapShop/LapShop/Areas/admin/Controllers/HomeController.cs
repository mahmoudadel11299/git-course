using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Area("admin")]
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
            return View();
        }
       
    }
}
