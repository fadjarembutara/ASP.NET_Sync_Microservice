using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Data;
using OrderServices.Dtos;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/order/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepo productRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            Console.WriteLine("--> Getting Product from OrderService");
            var productItems = await _productRepo.GetAllProducts();
            if(!productItems.Any())
            {
                return Ok("Belum ada produk, silahkan melakukan sinkronisasi terlebih dahulu");
            }
            return Ok(productItems);
        }

    }
}
