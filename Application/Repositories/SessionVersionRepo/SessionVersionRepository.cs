using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.Repositories.SessionVersionRepo
{
    public class SessionVersionRepository : ISessionVersionRepository
    {
        private readonly ApplicationDbContext _context;

        public SessionVersionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Creates a session version
        /// </summary>
        /// <param name="sessionVersion">Version Instance</param>
        /// <returns>The created session version</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<SessionVersion> CreateAsync(SessionVersion sessionVersion)
        {
            if (sessionVersion == null) throw new ArgumentNullException(nameof(sessionVersion));

            await _context.Versions.AddAsync(sessionVersion);
            await _context.SaveChangesAsync();

            return sessionVersion;
        }

        /// <summary>
        /// Gets a session version by its Id
        /// </summary>
        /// <param name="sessionVersionId"></param>
        /// <returns>The found session version</returns>
        public async Task<SessionVersion?> GetBySessionVersionIdAsync(int sessionVersionId)
        {
            return await _context.Versions
                .Where(s => s.Id == sessionVersionId)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Gets a session version by its name
        /// </summary>
        /// <param name="sessionName"></param>
        /// <returns>The found session version</returns>
        public async Task<SessionVersion?> GetBySessionIdVersionNameAsync(int sessionId, string sessionName)
        {
            return await _context.Versions
                .Where(s => s.SessionId == sessionId)
                .Where(s => s.Name == sessionName)
                .SingleOrDefaultAsync();
        }


        /// <summary>
        /// Gets a session version by its Id
        /// </summary>
        /// <param name="sessionVersionId"></param>
        /// <returns>The found session version</returns>
        public async Task<IEnumerable<SessionVersion>> GetBySessionIdAsync(int sessionId)
        {
            var session = await _context.Sessions
                .Include(s => s.SessionVersions)
                .Where(s => s.Id == sessionId)
                .SingleOrDefaultAsync();

            //TODO: Handle nulls ~Dan R.

            return session.SessionVersions;
        }
    }
}
