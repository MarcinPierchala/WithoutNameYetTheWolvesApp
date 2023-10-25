using AppUtilities;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;
using System.Diagnostics;
using WithoutNameYetTheWolvesApp.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WithoutNameYetTheWolvesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        string theAnswer = "";
        OpenAIAPI opeiAi = new OpenAIAPI(AppData.ApiKey);
        CompletionRequest completionRequest = new CompletionRequest();
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Chat([Bind("Prompt")] SearchModel searchModel)
        {
            if(searchModel.Prompt == null || searchModel.Prompt == "")
            {
                searchModel.Response = "Zadaj pytanie!";
                return View(searchModel);
            }
            
            //CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = searchModel.Prompt;
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

            searchModel.Response = theAnswer;

            return View(searchModel);
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