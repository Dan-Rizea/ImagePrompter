using Persistence.Entities;

namespace Application.Repositories.SessionRepo
{
    public interface ISessionRepository
    {
        public Task<Session> CreateAsync(Session session);
        public Task<Session?> GetBySessionIdAsync(Guid sessionId);
        public Task<Session?> GetBySessionIdVersionNameAsync(Guid sessionId, string versionName);
    }
}
