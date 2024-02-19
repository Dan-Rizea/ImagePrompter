using Application.Repositories.SessionRepo;
using Application.Repositories.SessionVersionRepo;
using Microsoft.AspNetCore.Components;
using Persistence.Entities;

namespace Application.Services.SessionLogic
{
    public class SessionLogicService : ISessionLogicService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionVersionRepository _sessionVersionRepository;
        private readonly NavigationManager _navigationManager;

        public SessionLogicService(ISessionRepository sessionRepository, 
            NavigationManager navigationManager, 
            ISessionVersionRepository sessionVersionRepository)
        {
            _sessionRepository = sessionRepository;
            _sessionVersionRepository = sessionVersionRepository;
            _navigationManager = navigationManager;
        }

        // ~TODO: Make method more generic ~Dan R.
        /// <inheritdoc cref="ISessionLogicService.InitializeNewSessionVersionAsync(string, string, byte[], Session)"/>
        public async Task<SessionVersion> InitializeNewSessionVersionAsync(string name, string prompt, byte[] image, Session currentSession)
        {
            var sessionVersionToCreate = new SessionVersion()
            {
                Image = image,
                Name = name,
                Prompt = prompt,
                SessionId = currentSession.Id
            };

            var createdVersion = await _sessionVersionRepository.CreateAsync(sessionVersionToCreate);

            return createdVersion;
        }

        /// <inheritdoc cref="ISessionLogicService.InitializeSessionAsync(bool)"/>
        public async Task<Session> InitializeSessionAsync(bool enableNavigation)
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

            var createdSession = await _sessionRepository.CreateAsync(newSession);

            if(enableNavigation) _navigationManager.NavigateTo($"/{createdSession.SessionId}/{newSessionVersion.Name}");

            return newSession;
        }

        /// <summary>
        /// Gets the template image for each new session
        /// </summary>
        /// <returns>An image</returns>
        /// <exception cref="Exception"></exception>
        private static async Task<byte[]> GetTemplateImageAsync()
        {
            //TODO: Make the path configurable ~ Dan R.
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "images", "template.png");

            try { return await File.ReadAllBytesAsync(path); }
            catch (Exception ex)
            {
                throw new Exception($"Error loading template image: {ex.Message}");
            }
        }
    }
}
