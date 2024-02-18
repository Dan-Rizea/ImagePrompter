using Application.DTOs;
using Persistence.Entities;

namespace Application.Services.LLM
{
    public interface ILLMServices
    {
        public Task<string> PromptAsync(string prompt, Session currentSession, SessionVersion currentSessionVersion);
        public Task<byte[]> GenerateImageAsync(string prompt);
    }
}
