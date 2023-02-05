using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStoreClient.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly HttpClient client = null;
        private string OrderDetailApiUrl = "";
        private string ProductApiUrl = "";
        private string OrderApiUrl = "";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderDetailController(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "https://localhost:44315/api/Order";
            ProductApiUrl = "https://localhost:44315/api/Product";
            OrderDetailApiUrl = "https://localhost:44315/api/OrderDetail";
            _httpContextAccessor = httpContextAccessor;
        }

        public ISession session { get { return _httpContextAccessor.HttpContext.Session; } }

        public async Task<IActionResult> Index()
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            HttpResponseMessage response = await client.GetAsync(OrderDetailApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<OrderDetail> listOrderDetails = JsonSerializer.Deserialize<List<OrderDetail>>(strData, options);
            return View(listOrderDetails);
        }

        public async Task<ActionResult> Details(int ordId, int proId)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            var orderDetail = await GetOrderDetailById(ordId, proId);
            if (orderDetail == null)
                return NotFound();
            return View(orderDetail);
        }

        private async Task<OrderDetail> GetOrderDetailById(int ordId, int proId)
        {
            HttpResponseMessage response = await client.GetAsync(OrderDetailApiUrl + "/" + ordId + "/" + proId);
            if (!response.IsSuccessStatusCode)
                return null;
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<OrderDetail>(strData, options);
        }

        public async Task<ActionResult> Create()
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            HttpResponseMessage responeOrder = await client.GetAsync(OrderApiUrl);
            string strData = await responeOrder.Content.ReadAsStringAsync();
            HttpResponseMessage responeProduct = await client.GetAsync(ProductApiUrl);
            string strData1 = await responeProduct.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Order> listOrder = JsonSerializer.Deserialize<List<Order>>(strData, options);
            ViewData["Orders"] = new SelectList(listOrder, "OrderId", "OrderId");
            List<Product> listProduct = JsonSerializer.Deserialize<List<Product>>(strData1, options);
            ViewData["Products"] = new SelectList(listProduct, "ProductId","ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderDetail o)
        {
            string data = JsonSerializer.Serialize(o);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(OrderDetailApiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Edit(int ordId, int proId)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            HttpResponseMessage response = await client.GetAsync(OrderDetailApiUrl + "/" + ordId + "/" + proId);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            OrderDetail orderDetail = JsonSerializer.Deserialize<OrderDetail>(strData, options);

            HttpResponseMessage responeCategory = await client.GetAsync(ProductApiUrl);
            string str = await responeCategory.Content.ReadAsStringAsync();

            List<Product> listProduct = JsonSerializer.Deserialize<List<Product>>(str, options);
            ViewData["Products"] = new SelectList(listProduct, "ProductId", "ProductName");

            HttpResponseMessage responeOrder = await client.GetAsync(OrderApiUrl);
            string str1 = await responeOrder.Content.ReadAsStringAsync();

            List<Order> listOrder = JsonSerializer.Deserialize<List<Order>>(str1, options);
            ViewData["Orders"] = new SelectList(listOrder, "OrderId", "OrderId");
            return View(orderDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int ordId, int proId, OrderDetail orderDetail)
        {
            string data = JsonSerializer.Serialize(orderDetail);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(OrderDetailApiUrl + "/" + ordId + "/" + proId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            HttpResponseMessage responeProduct = await client.GetAsync(ProductApiUrl);
            string strData = await responeProduct.Content.ReadAsStringAsync();
            HttpResponseMessage responeOrder = await client.GetAsync(OrderApiUrl);
            string strData1 = await responeOrder.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Product> listProduct = JsonSerializer.Deserialize<List<Product>>(strData, options);
            ViewData["Products"] = new SelectList(listProduct, "ProductId", "ProductName");
            List<Order> listOrder = JsonSerializer.Deserialize<List<Order>>(strData1, options);
            ViewData["Orders"] = new SelectList(listOrder, "OrderId", "OrderId");
            return View();
        }

        public async Task<ActionResult> Delete(int ordId, int proId)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            var orderDetail = await GetOrderDetailById(ordId, proId);
            if (orderDetail == null)
                return NotFound();
            return View(orderDetail);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ordId, int proId)
        {
            var orderDetail = await GetOrderDetailById(ordId, proId);

            HttpResponseMessage response = await client.DeleteAsync(OrderDetailApiUrl + "/" + ordId + "/" + proId);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }

}
