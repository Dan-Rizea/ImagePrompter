using Persistence.Entities;

namespace Application.Repositories.SessionVersionRepo
{
    public interface ISessionVersionRepository
    {
        /// <summary>
        /// Creates a new session version
        /// </summary>
        /// <param name="sessionVersion">The session version object to create</param>
        /// <returns>The new session version</returns>
        public Task<SessionVersion> CreateAsync(SessionVersion sessionVersion);

        /// <summary>
        /// Gets all session versions by their corresponding session ID
        /// </summary>
        /// <param name="sessionId">The provided session ID</param>
        /// <returns>All session versions of a given session</returns>
        public Task<IEnumerable<SessionVersion>> GetBySessionIdAsync(int sessionId);

        /// <summary>
        /// Gets a session version by its session ID and session version name
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="sessionName"></param>
        /// <returns></returns>
        public Task<SessionVersion?> GetBySessionIdVersionNameAsync(int sessionId, string sessionName);
    }
}
