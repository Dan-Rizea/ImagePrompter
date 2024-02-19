using Persistence.Entities;

namespace Application.Repositories.SessionRepo
{
    public interface ISessionRepository
    {
        /// <summary>
        /// Creates a new session
        /// </summary>
        /// <param name="session">Session object</param>
        /// <returns>A new session</returns>
        public Task<Session> CreateAsync(Session session);
        
        /// <summary>
        /// Gets a session by its ID
        /// </summary>
        /// <param name="sessionId">The ID of the session</param>
        /// <returns>The queried session</returns>
        public Task<Session?> GetBySessionIdAsync(Guid sessionId);
        
        /// <summary>
        /// Gets a session by its ID
        /// </summary>
        /// <param name="sessionId">The ID of the session</param>
        /// <param name="sessionVersionName">The name of the version</param>
        /// <returns>The queried session</returns>
        public Task<Session?> GetBySessionIdVersionNameAsync(Guid sessionId, string sessionVersionName);
    }
}
