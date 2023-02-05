using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetails()
        {
            var listOrderDetails = new List<OrderDetail>();
            try
            {
                using (var context = new MyDbContext())
                {
                    listOrderDetails = context.OrderDetails.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrderDetails;
        }

        public static OrderDetail FindOrderDetailById(int ordId, int proId)
        {
            OrderDetail o = new OrderDetail();
            try
            {
                using (var context = new MyDbContext())
                {
                    o = context.OrderDetails.SingleOrDefault(x => x.OrderId == ordId &&
                                                                    x.ProductId == proId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return o;
        }

        public static void SaveOrderDetail(OrderDetail o)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.OrderDetails.Add(o);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateOrderDetail(OrderDetail o, int ordId, int proId)
        {
            try
            {
                using (var context = new MyDbContext())
                {

                    context.Entry<OrderDetail>(o).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteOrderDetail(OrderDetail o)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var o1 = context.OrderDetails.SingleOrDefault(c => c.OrderId == o.OrderId && c.ProductId == o.ProductId);
                    context.OrderDetails.Remove(o1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static IList<OrderDetail> GetOrderDetailListByListOrder(IList<Order> orders)
        {
            var listOrderDetail = new List<OrderDetail>();
            var filter = orders.ToList();
            var result = new List<OrderDetail>();
            try
            {
                using (var context = new MyDbContext())
                {
                    listOrderDetail = context.OrderDetails.ToList();
                    for (int i = 0; i < filter.Count(); i++)
                    {
                        for (int z = 0; z < listOrderDetail.Count(); z++)
                        {
                            if (filter[i].OrderId == listOrderDetail[z].OrderId)
                            {
                                result.Add(listOrderDetail[z]);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
        public static double GetStatistic(IList<Order> orders)
        {
            double total = 0.0;
            var x = GetOrderDetailListByListOrder(orders).ToList();
            foreach (var z in x)
            {
                total += z.Quantity * (double)z.UnitPrice - (z.Quantity * (double)z.UnitPrice * (z.Discount / 100));
            }
            return total;
        }
    }
}
