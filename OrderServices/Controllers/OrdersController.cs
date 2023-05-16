using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServices.Data;
using OrderServices.Dtos;
using OrderServices.Models;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderrepo;

        public OrdersController(IMapper mapper, IOrderRepo orderrepo)
        {
            _mapper = mapper;
            _orderrepo= orderrepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadAllOrder>>> GetOrders()
        {
            var order = await _orderrepo.GetAllOrder();
            if (!order.Any())
            {
                return Ok("Belum ada orderan, silahkan order terlebih dahulu");
            }
            var readOrder = _mapper.Map<IEnumerable<ReadAllOrder>>(order);
            return Ok(order);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            var resultCheckProduct = await _orderrepo.CheckProduct(createOrderDto.ProductId);
            if (!resultCheckProduct)
            {
                return BadRequest("Produk tidak ditemukan");
            }
            var checkStock = await _orderrepo.CheckProductStock(createOrderDto.ProductId, createOrderDto.Quantity);
            if (!checkStock)
            {
                return BadRequest("Produk tidak memiliki stok yang cukup");
            }
            var modelOrder = _mapper.Map<Order>(createOrderDto);
            modelOrder.OrderDate = DateTime.Now;
            await _orderrepo.Create(modelOrder);
            _orderrepo.SaveChanges();

            var readOrder = _mapper.Map<ReadOrderDto>(modelOrder);
            readOrder.TotalPay = await _orderrepo.TotalPay(modelOrder.ProductId, modelOrder.Quantity);
            await _orderrepo.ProductOut(modelOrder.ProductId, modelOrder.Quantity);
            return Ok(readOrder);
        }
    }
}
