using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace AppUtilities
{
    public class ChatConversation
    {
        
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

            if(theAnswer.Length == 0)
            {
                theAnswer = "wystąpił błąd - to do exception handling";
            }

            return theAnswer;
        }
    }
}