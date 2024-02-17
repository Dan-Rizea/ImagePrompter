using Application.Miscellaneous;
using Application.Models;
using Application.Services.Mailing;
using Application.Services.SessionData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Application.Services.LLM
{
    public class LLMServices : ILLMServices
    {
        // Consideration for SemanticKernel:
        // Due to the fact that the main use case for this application consists of image generation, I think that it might be a bit overkill.
        // Therefore, I am only going to use HttpRequests.

        //TODO: This class is getting cluttered. Split into helper methods containing the private functionalities ~ Dan R.

        private readonly IConfiguration _config;
        private readonly IMailingService _mailingService;
        private readonly ISessionDataService _sessionDataService;

        public LLMServices(IConfiguration config, IMailingService mailingService, ISessionDataService sessionDataService)
        {
            _config = config;
            _mailingService = mailingService;
            _sessionDataService = sessionDataService;
        }

        public async Task<string> PromptAsync(string prompt)
        {
            var promptType = await GetPromptTypeAsync(prompt);

            switch (promptType)
            {
                case PromptType.GenerateImage:
                    return await GenerateImageAsync(prompt);
                case PromptType.EditImage:
                    return await EditImageAsync(prompt);
                case PromptType.DownloadImage:
                    return await DownloadImageAsync(prompt);
                case PromptType.ResizeImage:
                    return await ResizeImageAsync(prompt);
                case PromptType.DescribeImage:
                    return await DescribeImageAsync(prompt);
                case PromptType.EmailImage:
                    await EmailImageAsync(prompt);
                    return "PLACEHOLDER";
                case PromptType.Error:
                default:
                    return "An unexpected error has occured. Please try again.";
            }
        }

        private async Task<PromptType> GetPromptTypeAsync(string prompt)
        {
            var pickedFunctionality = await QueryChatGPT(PromptTemplates.FilterPrompt, prompt);
            Enum.TryParse(pickedFunctionality, out PromptType promptType);
            //TODO: deserialize output into a json object of PromptType

            return promptType;
        }

        private async Task EmailImageAsync(string prompt)
        {
            var mailingDetails = await QueryChatGPT(PromptTemplates.MailingPrompt, prompt, true);

            var deserializedDetails = JsonConvert.DeserializeObject<MailingModel>(mailingDetails);

            if (deserializedDetails.Error) return;

            var imageToSend = _sessionDataService.CurrentSessionVersion.Image;

            await _mailingService.SendMailAsync(imageToSend,
                deserializedDetails.Email,
                deserializedDetails.Subject,
                deserializedDetails.MessageBody,
                deserializedDetails.CustomerName
            );
        }

        private async Task<string> DescribeImageAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        private async Task<string> ResizeImageAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        private async Task<string> DownloadImageAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        private async Task<string> EditImageAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GenerateImageAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method sends a prompt to GPT-4
        /// </summary>
        /// <param name="prompt">The prompt</param>
        /// <param name="jsonMode">Optional json_mode querying</param>
        /// <returns>String response</returns>
        /// <exception cref="NullReferenceException"></exception>
        private async Task<string> QueryChatGPT(string promptTemplate, string userPrompt, bool jsonMode = false)
        {
            var requestTime = TimeSpan.Parse(_config.GetSection("DefaultLLMTimeout").Value
                ?? throw new NullReferenceException("Please add an LLM timeout value."));

            var content = new StringContent($@"
                {{
                  ""model"": ""gpt-4-turbo-preview"",
                  ""messages"": [
                    {{
                      ""role"": ""system"",
                      ""content"": ""{promptTemplate}""
                    }},
                    {{
                      ""role"": ""user"",
                      ""content"": ""{userPrompt}""
                    }}
                  ],
                  ""temperature"": 0,
                  ""top_p"": 1,
                  ""n"": 1,
                  ""stream"": false,
                  ""max_tokens"": 4096,
                  ""presence_penalty"": 0,
                  ""frequency_penalty"": 0
                }}", Encoding.UTF8, "application/json");

            var apiKey = _config["LLMKeys:OpenAIAPIKey"];

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Accept", "application/json");

            client.Timeout = requestTime;

            request.Content = content;

            var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();

            var jsonResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
            return jsonResponse["choices"][0]["message"]["content"].Value<string>();
        }

    }
}
