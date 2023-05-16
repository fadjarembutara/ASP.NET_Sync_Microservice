using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Data;
using ProductServices.Dtos;
using ProductServices.Models;
using ProductServices.SyncDataServices.Http;

namespace ProductServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productrepo;
        private readonly IMapper _mapper;
        private readonly IOrderDataClient _orderDataClient;

        public ProductsController(IProductRepo productrepo, IMapper mapper, IOrderDataClient orderDataClient) 
        {
            _productrepo = productrepo;
            _mapper = mapper;
            _orderDataClient = orderDataClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts() 
        {
            Console.WriteLine("--> Getting Product <--");
            var productItem = await _productrepo.GetAllProduct();
            return Ok(productItem);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByProductId(int id)
        {
            var product = await _productrepo.GetById(id);
            var readProduct = _mapper.Map<ReadProductDto>(product);
            return Ok(readProduct);
        }
        [HttpGet("/find/{name}")]
        public async Task<ActionResult> GetByProductName(string name)
        {
            var product = await _productrepo.GetByName(name);
            var readProduct = _mapper.Map<ReadProductDto>(product);
            return Ok(readProduct);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                var product = _mapper.Map<Product>(updateProductDto);
                product.ProductId = id;
                await _productrepo.Update(id, product);
                _productrepo.SaveChanges();
                var readProductDto = _mapper.Map<ReadProductDto>(product);
                return Ok(readProductDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var productModel = _mapper.Map<Product>(createProductDto);
            await _productrepo.Create(productModel);
            _productrepo.SaveChanges();

            var productReadDto = _mapper.Map<ReadProductDto>(productModel);

            return Ok(productReadDto);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try{
                await _productrepo.Delete(id);
                _productrepo.SaveChanges();
            }catch(Exception ex)
            {
                return BadRequest($"Tidak dapat menghapus produk : {ex.Message}");
            }
            
            return Ok("Berhasil menghapus produk");
        }

    }
}
