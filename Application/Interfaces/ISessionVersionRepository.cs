using Persistence.Entities;

namespace Application.Interfaces
{
    public interface ISessionVersionRepository
    {
        public Task<SessionVersion> CreateAsync(SessionVersion sessionVersion);
        public Task<SessionVersion?> GetBySessionIdAsync(int sessionVersionId);
    }
}
