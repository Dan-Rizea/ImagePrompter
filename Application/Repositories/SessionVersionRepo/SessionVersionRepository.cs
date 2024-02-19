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

        /// <inheritdoc cref="ISessionVersionRepository.CreateAsync(SessionVersion)"/>
        public async Task<SessionVersion> CreateAsync(SessionVersion sessionVersion)
        {
            if (sessionVersion == null) throw new ArgumentNullException(nameof(sessionVersion));

            await _context.Versions.AddAsync(sessionVersion);
            await _context.SaveChangesAsync();

            return sessionVersion;
        }

        /// <inheritdoc cref="ISessionVersionRepository.GetBySessionIdVersionNameAsync(int, string)"/>
        public async Task<SessionVersion?> GetBySessionIdVersionNameAsync(int sessionId, string sessionName)
        {
            return await _context.Versions
                .Where(s => s.SessionId == sessionId)
                .Where(s => s.Name == sessionName)
                .SingleOrDefaultAsync();
        }

        /// <inheritdoc cref="ISessionVersionRepository.GetBySessionIdAsync(int)(int, string)"/>
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
