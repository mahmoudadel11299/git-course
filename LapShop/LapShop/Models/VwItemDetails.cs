namespace LapShop.Models
{
    public class VwItemDetails
    {
        public VwItemDetails()
        {
            Item = new VwItem();
            lstItemImages = new List<TbItemImage>();
            lstRecommendedItems = new List<VwItem>();
        }
        public VwItem Item { get; set; }
        public List<TbItemImage> lstItemImages { get; set; }
        public List<VwItem> lstRecommendedItems { get; set; }
    }
}
