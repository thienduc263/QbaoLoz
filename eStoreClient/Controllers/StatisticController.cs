using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class StatisticController : Controller
    {
        private readonly HttpClient client = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string OrderApiUrl = "";
        private string OrderDetailApiUrl = "";

        public StatisticController(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "https://localhost:44315/api/Order";
            OrderDetailApiUrl = "https://localhost:44315/api/OrderDetail";
            _httpContextAccessor = httpContextAccessor;
        }
        public ISession session { get { return _httpContextAccessor.HttpContext.Session; } }

        public async Task<IActionResult> Index(DateTime startDate, DateTime endDate)
        {
            if (session.GetString("User") != null && session.GetString("Role") == "Admin")
            {
                var stringStartDate = startDate.ToString("yyyy-MM-dd");
                var stringEndDate = endDate.ToString("yyyy-MM-dd");
                HttpResponseMessage responseOrder = await client.GetAsync(OrderApiUrl + "/GetOrdersStatistic?startDate=" + stringStartDate.ToString().Trim() + "&endDate=" + stringEndDate.ToString().Trim());
                HttpResponseMessage responseOrderDetail = await client.GetAsync(OrderDetailApiUrl + "/GetOrderDetailListByListOrder?startDate=" + stringStartDate.ToString().Trim() + "&endDate=" + stringEndDate.ToString().Trim());
                HttpResponseMessage responseTotal = await client.GetAsync(OrderDetailApiUrl + "/GetOrderDetailStatistic?startDate=" + stringStartDate.ToString().Trim() + "&endDate=" + stringEndDate.ToString().Trim());
                if (responseOrder.IsSuccessStatusCode && responseOrderDetail.IsSuccessStatusCode && responseTotal.IsSuccessStatusCode)
                {
                    string strDataOrder = await responseOrder.Content.ReadAsStringAsync();
                    string strDataOrderDetail = await responseOrderDetail.Content.ReadAsStringAsync();
                    string strDataTotal = await responseTotal.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    List<Order> listOrders = JsonSerializer.Deserialize<List<Order>>(strDataOrder, options);
                    List<OrderDetail> listOrderDetail = JsonSerializer.Deserialize<List<OrderDetail>>(strDataOrderDetail, options);
                    double total = JsonSerializer.Deserialize<double>(strDataTotal, options);

                    ViewBag.listOrders = listOrders;
                    ViewBag.listOrderDetail = listOrderDetail;
                    ViewBag.total = total;
                    return View();
                }

                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
