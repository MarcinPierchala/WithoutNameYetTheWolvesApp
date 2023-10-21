using AppUtilities;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;
using System.Diagnostics;
using WithoutNameYetTheWolvesApp.Models;

namespace WithoutNameYetTheWolvesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        public async Task<string> TalkWithChat(string query)
        {
            string theAnswer = "";
            var opeiAi = new OpenAIAPI(AppData.ApiKey);
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = query;
            completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 1024;

            var completions = await opeiAi.Completions.CreateCompletionAsync(completionRequest);

            foreach (var completion in completions.Completions)
            {
                theAnswer += completion.Text;
            }

            if (theAnswer.Length == 0)
            {
                theAnswer = "wystąpił błąd - to do exception handling";
            }

            return theAnswer;
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