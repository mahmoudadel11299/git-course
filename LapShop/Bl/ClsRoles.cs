using LapShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public interface Iroles
    {
        public List<TbItem> GetAll();
       
        public TbItem GetById(int id);
        public bool Save(TbItem item);
        public bool Dekete(int id);
    }
    public class ClsRoles:Iroles
    {
        LapShopContext context;
        public ClsRoles(LapShopContext ctx)
        {
            context = ctx;
        }
        public List<TbItem> GetAll()
        {

            try
            {

                var lstCategories = context.TbItems.ToList();
                return lstCategories;
            }
            catch
            {
                return new List<TbItem>();
            }
        }



        public TbItem GetById(int id)
        {
            try
            {
                var item = context.TbItems.FirstOrDefault(a => a.ItemId == id);
                return item;
            }
            catch
            {
                return new TbItem();
            }
        }


        public bool Save(TbItem item)
        {
            try
            {
                if (item.ItemId == 0)
                {
                   
                    context.TbItems.Add(item);
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

        public bool Dekete(int id)
        {
            try
            {
                var item = GetById(id);
                if (item != null)
                {
                  
                    context.SaveChanges();


                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
