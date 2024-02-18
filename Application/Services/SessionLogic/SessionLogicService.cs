using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Components;
using Persistence.Entities;

namespace Application.Services.SessionLogic
{
    public class SessionLogicService : ISessionLogicService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly NavigationManager _navigationManager;
        private readonly ISessionVersionRepository _sessionVersionRepository;

        public SessionLogicService(ISessionRepository sessionRepository, 
            NavigationManager navigationManager, 
            ISessionVersionRepository sessionVersionRepository)
        {
            _sessionRepository = sessionRepository;
            _navigationManager = navigationManager;
            _sessionVersionRepository = sessionVersionRepository;
        }

        // ~TODO: Make method more generic ~Dan R.
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
