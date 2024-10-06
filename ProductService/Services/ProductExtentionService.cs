
using ProductService.Repository;

namespace ProductService.Services
{
    public class ProductExtentionService : IProductExtentionService
    {
        private readonly IProductRepository _repository;

        public ProductExtentionService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetProductImageAsync(int productId)
        {
            var item=await _repository.GetByID(productId);
            return item.ImageUrl ?? "";
        }
    }
}
