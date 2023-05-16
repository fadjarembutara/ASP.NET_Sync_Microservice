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
    public class SyncController : Controller
    {
        private readonly IProductRepo _productrepo;
        private readonly IMapper _mapper;
        private readonly IOrderDataClient _orderDataClient;

        public SyncController(IProductRepo productrepo, IMapper mapper, IOrderDataClient orderDataClient) 
        {
            _productrepo = productrepo;
            _mapper = mapper;
            _orderDataClient = orderDataClient;
        }

        [HttpPost]
        public async Task<ActionResult> SyncProducts()
        {
            try
            {
                await _productrepo.ProductOut();
                return Ok("Sinkronisasi produk berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest($"Tidak dapat melakukan sinkronisasi: {ex.Message}");
            }
        }
    }
}