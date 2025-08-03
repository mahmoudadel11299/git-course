using LapShop.Models;

namespace LapShop.Bl
{
    public interface IItemImages
    {
        public List<TbItemImage> GetByItemId(int id);
        public bool Save(TbItemImage item);

    }

    public class ClsItemImages : IItemImages
    {
        LapShopContext context;
        public ClsItemImages(LapShopContext ctx)
        {
            context = ctx;
        }

        public List<TbItemImage> GetByItemId(int id)
        {
            try
            {
                var item = context.TbItemImages.Where(a => a.ItemId == id).ToList();
                return item;
            }
            catch
            {
                return new List<TbItemImage>();
            }
        }
        public bool Save(TbItemImage item)
        {
            try
            {
                if (item.ItemId == 0)
                {
                    
                    context.TbItemImages.Add(item);
                }
                else
                {
                   
                    context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
