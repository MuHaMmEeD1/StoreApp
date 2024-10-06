using BarCodeService.DataContext;
using BarCodeService.Dtos;
using BarCodeService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarCodeService.Repositories
{
    public class BarcodeRepository : IBarcodeRepository
    {
        private readonly BarcodeDataContext _context;

        public BarcodeRepository(BarcodeDataContext context)
        {
            _context = context;
        }

        public async Task<string> AddBarcodeAsync(ProductItemDto dto)
        {
            var item=await _context.Barcodes.FirstOrDefaultAsync(b=>b.ProductId==dto.ProductId && b.Volume==dto.Volume);
            if (item == null)
            {
                var data = new Barcode
                {
                    ProductName = dto.ProductName,
                    Volume = dto.Volume,
                    ProductId = dto.ProductId,
                    TotalPrice = dto.Volume * dto.Price
                };
                data.Code = data.ToString();
                await _context.Barcodes.AddAsync(data);
                await _context.SaveChangesAsync();
                return data.Code;
            }
            return item.Code ?? "";
        }
    }
}
