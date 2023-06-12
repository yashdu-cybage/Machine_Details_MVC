using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http; 
namespace MVC_Project.Controllers
{
    public class MVCProjectController : Controller
    {
        private readonly HttpClient _httpClient;

        public MVCProjectController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {

            var response = await _httpClient.GetAsync("https://localhost:7197/api/Machine");
            var resultString = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(resultString);
            return View(result);
        }
    }
}
