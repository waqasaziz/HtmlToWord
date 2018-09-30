using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordDictionaryService _WordDictionaryService;

        public HomeController(IWordDictionaryService WordDictionaryService)
        {
            _WordDictionaryService = WordDictionaryService;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index([FromBody]string url)
        {

            var wordDictionary = await _WordDictionaryService.GetWordsAsync(url, 100);
            var result = wordDictionary.Select(x => new
            {
                text = x.Key,
                weight = x.Value
            });

            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
