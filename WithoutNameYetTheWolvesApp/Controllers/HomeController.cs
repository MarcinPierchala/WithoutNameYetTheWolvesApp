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
        string category = "";
        OpenAIAPI opeiAi = new OpenAIAPI(AppData.ApiKey);
        CompletionRequest completionRequest = new CompletionRequest();
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chat()
        {
            SearchModel searchModel = new SearchModel();
            searchModel.Prompt = "Tutaj zadaj swoje pytanie";
            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> Chat([Bind("Prompt", "Category")] SearchModel searchModel)
        {
            if(searchModel.Prompt == null || searchModel.Prompt == "" || searchModel.Prompt == "Tutaj zadaj swoje pytanie")
            {
                searchModel.Response = "Zadaj pytanie!";
                return View(searchModel);
            }
            
            if(searchModel.Category == null)
            {
                completionRequest.Prompt = searchModel.Prompt;
            }
            else
            {
                completionRequest.Prompt = searchModel.Category.ToString() + searchModel.Prompt;
            }
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

            searchModel.Response = "You: " + "\nKategoria zapytania: " + searchModel.Category + "\n" + searchModel.Prompt + "\n\n" + "Chat: " + theAnswer;
            searchModel.Prompt = "";

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