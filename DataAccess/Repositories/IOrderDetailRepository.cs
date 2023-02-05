using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail o);
        OrderDetail GetOrderDetailById(int ordId, int proId);
        void DeleteOrderDetail(OrderDetail o);
        void UpdateOrderDetail(OrderDetail o, int ordId, int proId);
        List<OrderDetail> GetOrderDetails();
        IList<OrderDetail> GetOrderDetailListByListOrder(IList<Order> orders);
        double GetStatistic(IList<Order> orders);
    }
}
