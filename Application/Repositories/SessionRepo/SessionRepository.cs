using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.Repositories.SessionRepo;
public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc cref="ISessionRepository.CreateAsync(Session)"/>
    public async Task<Session> CreateAsync(Session session)
    {
        if (session == null) throw new ArgumentNullException(nameof(session));

        await _context.Sessions.AddAsync(session);
        await _context.SaveChangesAsync();

        return session;
    }

    /// <inheritdoc cref="ISessionRepository.GetBySessionIdAsync(Guid)"/>
    public async Task<Session?> GetBySessionIdAsync(Guid sessionId)
    {
        return await _context.Sessions
            .Include(s => s.SessionVersions)
            .Where(s => s.SessionId == sessionId)
            .SingleOrDefaultAsync();
    }

    /// <inheritdoc cref="ISessionRepository.GetBySessionIdVersionNameAsync(Guid, string)"/>
    public async Task<Session?> GetBySessionIdVersionNameAsync(Guid sessionId, string sessionVersionName)
    {
        return await _context.Sessions
            .Include(s => s.SessionVersions.Where(sv => sv.Name == sessionVersionName))
            .Where(s => s.SessionId == sessionId)
            .SingleOrDefaultAsync();
    }
}
