using LapShop.Models;

namespace LapShop.Apicontroller
{

    public  class TbItemType
    {
        public int ItemTypeId { get; set; }

        public string ItemTypeName { get; set; } = null!;

        public string? ImageName { get; set; }

        public int CurrentState { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
    }
    public class ApiResponsee
    {
        // data coming from api
        public List<TbItem> data { get; set; }
        // list of api errors
        public object errors { get; set; }
        // api status code 200=sucess 400=failed
        public string statusCode { get; set; }
    }

    public class api
    {
        static HttpClient client= new HttpClient();

        public async Task<List<TbItem>> getproduct(string path)
        {
            ApiResponsee myresponse=null;
            HttpResponseMessage  response= await client.GetAsync(path); 
            if(response.IsSuccessStatusCode)
            {
                myresponse=await response.Content.ReadAsAsync<ApiResponsee>();   
            }
            return myresponse.data;
        }

        public async Task<Uri> createproduct(TbItem item)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://localhost:7141/api/items/", item);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }
    }
}
