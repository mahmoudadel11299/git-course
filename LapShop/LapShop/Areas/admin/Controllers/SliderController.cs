using LapShop.Bl;
using LapShop.Models;
using LapShop.Utlities;
using Microsoft.AspNetCore.Mvc;

namespace LapShop.Areas.admin.Controllers
{
    [Area("admin")]
    public class SliderController : Controller
    {
        ISliders oClsIslider;
        public SliderController(ISliders oIslider)
        {
            oClsIslider = oIslider;
        }
        public IActionResult Edit()
        {
            var item = new TbSlider();


            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbSlider item, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
                return View("Edit", item);

            item.ImageName = await Helper.UploadImage(Files, "Slider");

            oClsIslider.Save(item);

            return RedirectToAction("Edit");
        }
    }
}
