using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Intranet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Intranet.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]AuthenticationViewModel auth, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return BadRequest(); 

            var content = JsonConvert.SerializeObject(auth);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await client.PostAsync("http://localhost:61031/user/authenticate", new StringContent(content, Encoding.Default, "application/json"));
            if (response.IsSuccessStatusCode) {
                var aa = response.Content.ReadAsStringAsync().Result;
            }
            return null;
        }

    }
}