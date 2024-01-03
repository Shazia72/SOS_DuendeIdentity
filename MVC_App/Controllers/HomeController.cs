using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_App.Models;
using MVC_App.Services;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MVC_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly ITokenService _tokenService;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_tokenService = tokenService;
        }
        [Authorize]
        public async Task<IActionResult> WebApi()
        {
            using var client = new HttpClient();
            //var token = await _tokenService.GetToken("WebAPI.read");
            var token = await HttpContext.GetTokenAsync("access_token");
            client.SetBearerToken(token);   

            var result = await client.GetAsync("https://localhost:5445/WeatherForecast");// calling webapi
            if (result.IsSuccessStatusCode)
            {
                var model = await result.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<WeatherData>>(model);
                return View(data);
            }
            throw new Exception("Unable to get content");
        }
        public IActionResult Index()
        {
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