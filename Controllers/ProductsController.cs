using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Domain.Services;
using Supermarket.API.Models;

namespace Supermarket.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger, IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> GetAllProduct()
        {
            _logger.LogInformation("fetching all the products");
            var products = await _productService.ListAsync();
            var resource = _mapper.Map<IEnumerable<Products>, IEnumerable<ProductResource>>(products);
            return resource;
        }
    }
}
