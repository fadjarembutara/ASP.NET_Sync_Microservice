using Microsoft.AspNetCore.Mvc;
using OrderServices.Models;

namespace OrderServices.Data
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _context;
        public OrderRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product != null;
        }

        public async Task<bool> CheckProductStock(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            return product.Stock >= quantity;
        }
        public Task Create(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.Orders.Add(order);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
            return _context.Orders.ToList();
        }

        public async Task<int> TotalPay(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            var payAmount = product.Price * quantity;
            return payAmount;
        }

        public async Task ProductOut(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.Stock -= quantity;
                await _context.SaveChangesAsync();
            }
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
