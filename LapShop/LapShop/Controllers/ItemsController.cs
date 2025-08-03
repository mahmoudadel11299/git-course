using LapShop.Bl;
using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using System.Diagnostics;

namespace LapShop.Controllers
{
    public class ItemsController:Controller
    {
        IItems oItem;
        IItemImages oItemImages;
        public ItemsController(IItems iItem, IItemImages oItemImages)
        {
            oItem = iItem;
            this.oItemImages = oItemImages;
        }

        public IActionResult ItemDetails(int id)
        {
            var item = oItem.GetItemId(id);

            VwItemDetails vm = new VwItemDetails();
            vm.Item = item;
            vm.lstRecommendedItems = oItem.GetRecommendedItems(id).ToList();
            vm.lstItemImages = oItemImages.GetByItemId(id);
            return View(vm);
        }
      

        public IActionResult ItemList()
        {
            return View();
        }

    }
}
