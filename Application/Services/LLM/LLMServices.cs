using Application.Miscellaneous.Prompting;
using Application.Models;
using Application.Services.Mailing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Persistence.Entities;
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

        public LLMServices(IConfiguration config, 
            IMailingService mailingService) 
        {
            _config = config;
            _mailingService = mailingService;
        }

        public async Task<string> PromptAsync(string prompt, Session currentSession, SessionVersion currentSessionVersion)
        {
            var promptType = await GetPromptTypeAsync(prompt);

            switch (promptType)
            {
                case PromptType.GenerateImage:
                    return promptType.ToString();
                case PromptType.EditImage:
                    return await EditImageAsync(prompt);
                case PromptType.DownloadImage:
                    return promptType.ToString();
                case PromptType.EmailImage:
                    await EmailImageAsync(prompt, currentSessionVersion.Image);
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

        private async Task EmailImageAsync(string prompt, byte[] image)
        {
            var mailingDetails = await QueryChatGPT(PromptTemplates.MailingPrompt, prompt, true);

            var deserializedDetails = JsonConvert.DeserializeObject<MailingModel>(mailingDetails);

            if (deserializedDetails.Error) return;

            await _mailingService.SendMailAsync(image,
                deserializedDetails.Email,
                deserializedDetails.Subject,
                deserializedDetails.MessageBody,
                deserializedDetails.CustomerName
            );
        }

        private async Task<string> EditImageAsync(string prompt)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> GenerateImageAsync(string prompt)
        {
            const string Url = "https://api.stability.ai/v1/generation/stable-diffusion-xl-1024-v1-0/text-to-image";
            var apiKey = _config["LLMKeys:StabilityAIAPIKey"];

            var body = new
            {
                steps = 40,
                width = 1024,
                height = 1024,
                seed = 0,
                cfg_scale = 5,
                samples = 1,
                text_prompts = new[]
                {
                    new
                    {
                        text = prompt,
                        weight = 1
                    },
                    new
                    {
                        text = "blurry, bad",
                        weight = -1
                    }
                }
            };

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Url);
                request.Headers.Add("Authorization", apiKey);
                request.Headers.Add("Accept", "application/json");

                request.Content = new StringContent(JsonConvert.SerializeObject(body), null, "application/json");

                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Non-200 response: {await response.Content.ReadAsStringAsync()}");
                }

                var data = JObject.Parse(await response.Content.ReadAsStringAsync());
                var image = data["artifacts"][0]["base64"].Value<string>();

                return Convert.FromBase64String(image);
            }
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

            //TODO: Make model configurable
            promptTemplate.Replace("\n", "");
            userPrompt = userPrompt.Replace("\n", "");

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

            using (var client = new HttpClient())
            {
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
}
