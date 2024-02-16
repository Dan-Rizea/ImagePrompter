using Application.Miscellaneous;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Application.Services
{
    public class LLMServices : ILLMServices
    {
        // Consideration for SemanticKernel:
        // Due to the fact that the main use case for this application consists of image generation, I think that it might be a bit overkill.
        // Therefore, I am only going to use HttpRequests.

        private readonly IConfiguration _config;

        public LLMServices(IConfiguration config)
        {
            _config = config;
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
                    return await EmailImageAsync(prompt);
                case PromptType.AnimateImage:
                    return await AnimateImageAsync(prompt);
                case PromptType.Error:
                default:
                    return "An unexpected error has occured. Please try again.";
            }
        }

        private async Task<PromptType> GetPromptTypeAsync(string prompt)
        {
            var pickedFunctionality = await QueryChatGPT(PromptTemplates.FilterPrompt + "{{" + prompt + "}}");
            Enum.TryParse(pickedFunctionality, out PromptType promptType);

            return promptType;
        }

        private async Task<string> AnimateImageAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        private async Task<string> EmailImageAsync(string prompt)
        {
            throw new NotImplementedException();
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

        private async Task<string> QueryChatGPT(string prompt)
        {
            var requestTime = TimeSpan.Parse("00:00:30");
            var data = new
            {
                model = "gpt-4-0125-preview",
                messages = new[] { prompt },
                temperature = 0
            };

            var client = new HttpClient();

            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

            var apiKey = _config["LLMKeys:OpenAIAPIKey"];

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer { apiKey }");
            client.Timeout = requestTime;

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", dataContent);

            var jsonResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
            return jsonResponse["choices"][0]["message"]["content"].Value<string>();
        }
    }
}
