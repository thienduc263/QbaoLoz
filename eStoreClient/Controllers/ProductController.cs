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
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";
        private string CategoryApiUrl = "";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44315/api/Product";
            CategoryApiUrl = "https://localhost:44315/api/Category";
            _httpContextAccessor = httpContextAccessor;
        }

        public ISession session { get { return _httpContextAccessor.HttpContext.Session; } }

        public async Task<IActionResult> Index(string searchString)
        {

            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            HttpResponseMessage response;
            if (string.IsNullOrEmpty(searchString))
            {
                response = await client.GetAsync(ProductApiUrl);
            }
            else
            {
                response = await client.GetAsync(ProductApiUrl + "/Search?search=" + searchString.ToLower());
            }
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);

            return View(listProducts);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            var product = await GetProductById(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        public async Task<ActionResult> Create()
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            HttpResponseMessage responeCategory = await client.GetAsync(CategoryApiUrl);
            string strData = await responeCategory.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Category> listCategory = JsonSerializer.Deserialize<List<Category>>(strData, options);
            ViewData["Categories"] = new SelectList(listCategory, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product p)
        {
            string data = JsonSerializer.Serialize(p);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(ProductApiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            HttpResponseMessage responeCategory = await client.GetAsync(CategoryApiUrl);
            string strData = await responeCategory.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Category> listCategory = JsonSerializer.Deserialize<List<Category>>(strData, options);
            ViewData["Categories"] = new SelectList(listCategory, "CategoryId", "CategoryName");
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Product product = JsonSerializer.Deserialize<Product>(strData, options);

            HttpResponseMessage responeCategory = await client.GetAsync(CategoryApiUrl);
            string str = await responeCategory.Content.ReadAsStringAsync();

            List<Category> listCategory = JsonSerializer.Deserialize<List<Category>>(str, options);
            ViewData["Categories"] = new SelectList(listCategory, "CategoryId", "CategoryName");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            string data = JsonSerializer.Serialize(product);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(ProductApiUrl + "/" + id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            HttpResponseMessage responeCategory = await client.GetAsync(CategoryApiUrl);
            string strData = await responeCategory.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Category> listCategory = JsonSerializer.Deserialize<List<Category>>(strData, options);
            ViewData["Categories"] = new SelectList(listCategory, "CategoryId", "CategoryName");
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            var product = await GetProductById(id);
            if (product == null)
                return NotFound();
            await SetViewData();
            return View(product);
        }

        public async Task SetViewData()
        {
            var listCategory = await GetCategory();
            ViewData["Categories"] = new SelectList(listCategory, "CategoryId", "CategoryName");
        }

        private async Task<Product> GetProductById(int id)
        {   
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/" + id);
            if (!response.IsSuccessStatusCode)
                return null;
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Product>(strData, options);
        }

        public async Task<IEnumerable<Category>> GetCategory()
        {
            HttpResponseMessage response = await client.GetAsync(CategoryApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Category> listCategory = JsonSerializer.Deserialize<List<Category>>(strData, options);
            return listCategory;
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await GetProductById(id);

            HttpResponseMessage response = await client.DeleteAsync(ProductApiUrl + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
