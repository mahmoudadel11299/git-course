using LapShop.Bl;
using LapShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Area("admin")]
    public class ajaxController : Controller
    {
        IItems oclsiitem;
        public ajaxController(IItems oiitem)
        {
            oclsiitem = oiitem;
        }
        public IActionResult List()
        {

            return View();
        }
        public IActionResult Edite()
        {
            return View(new TbItem());
        }
        public IActionResult Save(TbItem item)
        {
            return View();
        }
    }
}
