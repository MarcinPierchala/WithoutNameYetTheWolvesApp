using Microsoft.AspNetCore.Components;
using WithoutNameYetTheWolvesApp.Models;

namespace WithoutNameYetTheWolvesApp.Views.Home
{
    public class Chat
    {
        private SearchModel searchModel = new();
        private bool isExecuting = false;
        [Inject] private IHttpClientFactory ClientFactory { get; set; }
        [Inject] private IConfiguration Configuration { get; set; }

        protected async Task Execute()
        {
            if (string.IsNullOrEmpty(searchModel.Prompt))
            {
                searchModel.Prompt = "Hej, zadaj jakieś pytanie";
            }

            isExecuting = true;

            HttpRequestMessage request = new(HttpMethod.Post, "https://api.openai.com/v1/completions"); 
        }
    }
}
