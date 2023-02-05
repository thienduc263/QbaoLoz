using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(OrderDetail o) => OrderDetailDAO.DeleteOrderDetail(o);

        public OrderDetail GetOrderDetailById(int ordId, int proId) => OrderDetailDAO.FindOrderDetailById(ordId, proId);

        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();

        public void SaveOrderDetail(OrderDetail o) => OrderDetailDAO.SaveOrderDetail(o);

        public void UpdateOrderDetail(OrderDetail o, int ordId, int proId) => OrderDetailDAO.UpdateOrderDetail(o, ordId, proId);

        public IList<OrderDetail> GetOrderDetailListByListOrder(IList<Order> orders) => OrderDetailDAO.GetOrderDetailListByListOrder(orders);

        public double GetStatistic(IList<Order> orders) => OrderDetailDAO.GetStatistic(orders);
    }
}
