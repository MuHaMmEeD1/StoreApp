using BarCodeService.Dtos;
using BarCodeService.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BarCodeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcodeRepository _barcodeRepository;

        public BarcodeController(IBarcodeRepository barcodeRepository)
        {
            _barcodeRepository = barcodeRepository;
        }

        // POST api/<BarcodeController>
        [HttpPost("AddBarcode")]
        public async Task<IActionResult> Post([FromBody] ProductItemDto dto)
        {
            var code=await _barcodeRepository.AddBarcodeAsync(dto);
            return Ok(new {Data = code});    
        }
    }
}
