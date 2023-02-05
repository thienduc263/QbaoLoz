using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        void SaveOrder(Order o);
        Order GetOrderById(int id);
        void DeleteOrder(Order o);
        void UpdateOrder(Order o);
        List<Order> GetOrders();
        List<Order> Filter(DateTime startDate, DateTime endDate);
    }
}
