using Persistence.Entities;

namespace Application.Services.SessionLogic
{
    public interface ISessionLogicService
    {
        public Task<SessionVersion> InitializeNewSessionVersionAsync(string name, string prompt, byte[] image, Session currentSession);
        public Task<Session> InitializeSessionAsync(bool enableNavigation = true);
    }
}
