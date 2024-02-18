using Persistence.Entities;

namespace Application.Interfaces
{
    public interface ISessionVersionRepository
    {
        public Task<SessionVersion> CreateAsync(SessionVersion sessionVersion);
        public Task<SessionVersion?> GetBySessionVersionIdAsync(int sessionVersionId);
        public Task<IEnumerable<SessionVersion>> GetBySessionIdAsync(int sessionId);
        public Task<SessionVersion?> GetBySessionIdVersionNameAsync(int sessionId, string sessionName);
    }
}
