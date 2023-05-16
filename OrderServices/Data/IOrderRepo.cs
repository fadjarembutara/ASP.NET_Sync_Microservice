using OrderServices.Models;
using System.Threading.Tasks;

namespace OrderServices.Data
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrder();
        Task Create(Order order);
        Task<bool> CheckProduct(int productId);
        Task<bool> CheckProductStock(int productId, int quantity);
        Task<int> TotalPay(int productId, int quantity);
        Task ProductOut(int productId, int quantity);
        bool SaveChanges();
    }
}
