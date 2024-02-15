using Persistence.Entities;

namespace Application.Interfaces
{
    public interface ISessionRepository
    {
        public Task<Session> CreateAsync(Session session);
        public Task<Session?> GetBySessionIdAsync(Guid sessionId);
        public Task<Session?> GetBySessionAndVersionIdAsync(Guid sessionId, int versionId);
    }
}
