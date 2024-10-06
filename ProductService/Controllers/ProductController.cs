using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Entities;
using ProductService.Repository;
using ProductService.Services;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductExtentionService _extentionService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductExtentionService extentionService, IProductRepository productRepository, IMapper mapper)
        {
            _extentionService = extentionService;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("GetImage/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var result = await _extentionService.GetProductImageAsync(id);
            return Ok(result);  
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            var result = await _productRepository.GetAll();
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(result);
            return dtos;
        }

        [HttpPost]
        public async Task<ProductDto> Post([FromBody] ProductDto productDto)
        {
            var item=_mapper.Map<Product>(productDto);
            await _productRepository.AddProduct(item);
            var dto = _mapper.Map<ProductDto>(item);

            return dto;
        }
    }
}
