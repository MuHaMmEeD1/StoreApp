using BarCodeService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarCodeService.DataContext
{
    public class BarcodeDataContext:DbContext
    {
        public BarcodeDataContext(DbContextOptions<BarcodeDataContext>options)
            :base(options)
        {
        }
        public DbSet<Barcode> Barcodes { get; set; }
    }
}
