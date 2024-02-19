using Persistence.Entities;

namespace Application.Services.LLM
{
    public interface ILLMServices
    {
        /// <summary>
        /// This method acts as a way to filter between different prompts and execute their associated actions
        /// </summary>
        /// <param name="prompt">User-provided prompt</param>
        /// <param name="currentSession">The current session the user is accessing</param>
        /// <param name="currentSessionVersion">The current session version the user is accessing</param>
        /// <returns>The prompt type or an error message</returns>
        public Task<string> PromptAsync(string prompt, Session currentSession, SessionVersion currentSessionVersion);
        /// <summary>
        /// This method is used to generate an image based on a user-provided prompt
        /// </summary>
        /// <param name="prompt">User-provided prompt</param>
        /// <returns>The generated image</returns>
        public Task<byte[]> GenerateImageAsync(string prompt);
    }
}
