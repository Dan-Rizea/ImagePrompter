# 🤖 Image Prompter 🤖
⚡Image playground based on natural language interaction⚡
## Application features
- [x] Create sessions that can be chained through subsequent versions that provide a trail to your prompting actions.
- [x] Generate images based on StabilityAI's chat-to-image model.
- [x] Download images by providing your e-mail address in your prompt.
- [x] Save images through prompts.
## Upcoming features
- [ ] Edit already existing images through prompts.
- [ ] Generate new sessions through prompts.
- [ ] Generate new versions through prompts.
- [ ] Resize images through prompts.
- [ ] Describe images through prompts.
- [ ] Animate images through prompts.
## Quick install [Deprecated as of Deployment]
All that's needed to setup this application locally is pasting and filling out these values in ImagePrompter's secret manager:
```
{
  "LLMKeys": {
    "OpenAIAPIKey": "[REDACTED]",
    "StabilityAIAPIKey": "[REDACTED]"
  },
  "MailSettings": {
    "Server": "[REDACTED]",
    "Port": [REDACTED],
    "SenderName": "[REDACTED]",
    "SenderEmail": "[REDACTED]",
    "UserName": "[REDACTED]",
    "Password": "[REDACTED]"
  },
  "ConnectionStrings": {
    "DefaultConnection": "[SQL Server Connection String]"
  }
}
```
## Application showcase
### Generate Images
![image](https://github.com/Dan-Rizea/ImagePrompter/assets/86754250/5a2a2a72-7ebd-4855-af0c-2d7ed61da1e2)
### Save Images
![image](https://github.com/Dan-Rizea/ImagePrompter/assets/86754250/1f424fde-670b-4f15-b505-6c2579c3dd08)
### Email Images
![image](https://github.com/Dan-Rizea/ImagePrompter/assets/86754250/df0ea971-adec-4699-8116-f1b0e256c95c)
