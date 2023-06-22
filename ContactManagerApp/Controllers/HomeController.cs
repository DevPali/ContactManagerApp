using ContactManagerApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactManagerApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(LoginRequest loginRequest)
        //{
        //    var content = JsonConvert.SerializeObject(loginRequest);
        //    var response = await _httpClient.PostAsync("/api/account/login", new StringContent(content, Encoding.Default, "application/json"));

        //    return RedirectToAction("Index", "Contacts");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}