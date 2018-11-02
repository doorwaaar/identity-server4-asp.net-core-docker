using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;

        public HomeController(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _configuration = configuration;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var httpClient = await _httpService.GetClient("http://localhost:32771");

            var response = await httpClient.GetAsync($"api/values").ConfigureAwait(false);

            var catsAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var catViewModel = JsonConvert.DeserializeObject<IEnumerable<string>>(catsAsString).ToList();
            return View(catViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}