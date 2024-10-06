
namespace SearchService.Services
{
    public class ProductService : IProductService
    {
        public string ImageUrl { get; set; }

        public async Task<string> GetProductImagePathAsync(int productId)
        {
            HttpClient httpClient=new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            string url = "https://localhost:10611/api/Product/GetImage/" + productId;
            response=await httpClient.GetAsync(url);
            var str=await response.Content.ReadAsStringAsync();
            return str;
        }
    }
}
