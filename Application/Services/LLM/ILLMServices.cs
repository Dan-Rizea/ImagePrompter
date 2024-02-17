namespace Application.Services.LLM
{
    public interface ILLMServices
    {
        public Task<string> PromptAsync(string prompt);
    }
}
