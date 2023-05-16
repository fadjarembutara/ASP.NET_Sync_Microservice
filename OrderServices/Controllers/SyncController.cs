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
    public class SyncController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public SyncController(IProductRepo productRepo,IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<ActionResult> SyncProducts()
        {
            try
            {
                await _productRepo.CreateProduct();
                return Ok("Produk berhasil disinkronkan");
            }
            catch (Exception ex)
            {
                return BadRequest($"Tidak dapat melakukan sinkronisasi: {ex.Message}");
            }
        }
    }
}