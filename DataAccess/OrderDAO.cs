using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class OrderDAO
    {
        public static List<Order> GetOrders()
        {
            var listOrders = new List<Order>();
            try
            {
                using (var context = new MyDbContext())
                {
                    listOrders = context.Orders.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listOrders;
        }

        public static Order FindOrderById(int ordId)
        {
            Order o = new Order();
            try
            {
                using (var context = new MyDbContext())
                {
                    o = context.Orders.SingleOrDefault(x => x.OrderId == ordId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return o;
        }

        public static void SaveOrder(Order o)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Orders.Add(o);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateOrder(Order o)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Order>(o).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteOrder(Order o)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var o1 = context.Orders.SingleOrDefault(c => c.OrderId == o.OrderId);
                    context.Orders.Remove(o1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Order> Filter(DateTime startDate, DateTime EndDate)
        {
            var orders = new List<Order>();
            var filter = new List<Order>();
            try
            {
                using (var db = new MyDbContext())
                {
                    orders = db.Orders.ToList();
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (orders[i].OrderDate >= startDate && orders[i].OrderDate <= EndDate)
                        {
                            filter.Add(orders[i]);
                        }
                    }
                    filter = filter.OrderByDescending(f => f.OrderDate).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return filter;
        }
    }
}

