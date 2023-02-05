using BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class MemberController : Controller
    {
        private readonly HttpClient client = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private string MemberApiUrl = "";

        public MemberController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUrl = "https://localhost:44315/api/Member";
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public ISession session { get { return _httpContextAccessor.HttpContext.Session; } }

        public async Task<IActionResult> Index()
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            HttpResponseMessage response = await client.GetAsync(MemberApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Member> listMembers = JsonSerializer.Deserialize<List<Member>>(strData, options);
            return View(listMembers);
        }

        public ActionResult Create()
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member m)
        {
            string data = JsonSerializer.Serialize(m);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(MemberApiUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            HttpResponseMessage response = await client.GetAsync(MemberApiUrl + "/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Member member = JsonSerializer.Deserialize<Member>(strData, options);

            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Member member)
        {
            string data = JsonSerializer.Serialize(member);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(MemberApiUrl + "/" + id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");

            var member = await GetMemberById(id);
            if (member == null)
                return NotFound();
            return View(member);
        }

        private async Task<Member> GetMemberById(int id)
        {
            HttpResponseMessage response = await client.GetAsync(MemberApiUrl + "/" + id);
            if (!response.IsSuccessStatusCode)
                return null;
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Member>(strData, options);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await GetMemberById(id);
            HttpResponseMessage response = await client.DeleteAsync(MemberApiUrl + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            if (session.GetString("User") == null) return RedirectToAction("Index", "Home");
            var member = await GetMemberById(id);
            if (member == null)
                return NotFound();
            return View(member);
        }

        public async Task<ActionResult> Login()
        {   
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginConfirmed(Member m, int id)
        {
            var getAdminEmail = _configuration.GetValue<string>("DefaultAccount:Email");
            var getAdminPassword = _configuration.GetValue<string>("DefaultAccount:Password");
            if (getAdminEmail.Equals(m.Email) && getAdminPassword.Equals(m.Password))
            {
                session.SetString("Role", "Admin");
                session.SetString("User", m.Email);
                return RedirectToAction("Index", "Home");
            }

            string data = JsonSerializer.Serialize(m);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response =  client.PostAsync(MemberApiUrl + "/Login", content).Result;

            if (response.IsSuccessStatusCode)
            {             
                session.SetString("Role", "User");
                session.SetString("User", m.Email);
                return RedirectToAction("Index", "Home");
            }
            if (m.Password != null || m.Email != null)
            {
                ViewBag.error = "Incorrect username or password. Try again!!!";
            }
            return View();
        }

        public ActionResult Logout()
        {
            session.Remove("Role");
            session.Remove("User");
            return RedirectToAction("Index", "Home");
        }
    }
}
