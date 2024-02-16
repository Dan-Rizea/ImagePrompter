using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Components;
using Persistence.Entities;

namespace ImagePrompter.Components.Logic
{
    public class SessionLogicService : ISessionLogicService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly NavigationManager _navigationManager;

        public SessionLogicService(ISessionRepository sessionRepository, NavigationManager navigationManager)
        {
            _sessionRepository = sessionRepository;
            _navigationManager = navigationManager;
        }

        public async Task<Session> InitializeSessionAsync()
        {
            var newSessionVersion = new SessionVersion()
            {
                Name = "v1",
                Prompt = "",
                Image = await GetTemplateImageAsync()
            };
            var newSessionVersions = new List<SessionVersion>() { newSessionVersion };

            var newSession = new Session()
            {
                SessionId = Guid.NewGuid(),
                SessionVersions = newSessionVersions
            };

            await _sessionRepository.CreateAsync(newSession);

            _navigationManager.NavigateTo($"/{newSession.SessionId}/{newSessionVersion.Name}");

            return newSession;
        }

        private static async Task<byte[]> GetTemplateImageAsync()
        {
            // Get the path to the template image
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "images", "template.png");

            // Use File.ReadAllBytesAsync for efficient asynchronous IO
            try
            {
                return await File.ReadAllBytesAsync(path);
            }
            catch (Exception ex)
            {
                // Handle errors gracefully, e.g., log the exception and return null
                throw new Exception($"Error loading template image: {ex.Message}");
            }
        }
    }
}
