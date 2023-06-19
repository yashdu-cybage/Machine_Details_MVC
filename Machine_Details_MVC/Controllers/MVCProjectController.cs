using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http; 
namespace MVC_Project.Controllers
{
    public class MVCProjectController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public MVCProjectController(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {

            var URL = _config.GetValue<string>("API_URL");
            var response = await _httpClient.GetAsync(URL);
            var resultString = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(resultString);
            return View(result);
        }
    }
}
