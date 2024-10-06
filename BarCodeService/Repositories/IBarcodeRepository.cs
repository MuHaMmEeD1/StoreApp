using BarCodeService.Dtos;

namespace BarCodeService.Repositories
{
    public interface IBarcodeRepository
    {
        Task<string> AddBarcodeAsync(ProductItemDto dto);
    }
}
