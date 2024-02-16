using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.Repositories;
public class SessionRepository : ISessionRepository
{
    private readonly ApplicationDbContext _context;

    public SessionRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Creates a session
    /// </summary>
    /// <param name="session">Session Instance</param>
    /// <returns>The created session</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<Session> CreateAsync(Session session)
    {
        if (session == null) throw new ArgumentNullException(nameof(session));
        
        await _context.Sessions.AddAsync(session);
        await _context.SaveChangesAsync();
        
        return session;
    }

    /// <summary>
    /// Gets a session by its Id
    /// </summary>
    /// <param name="sessionId"></param>
    /// <returns>The found session</returns>
    public async Task<Session?> GetBySessionIdAsync(Guid sessionId)
    {
        return await _context.Sessions
            .Include(s => s.SessionVersions)
            .Where(s => s.SessionId == sessionId)
            .SingleOrDefaultAsync();
    }

    /// <summary>
    /// Gets a session by its Id and by its version Id
    /// </summary>
    /// <param name="sessionId"></param>
    /// <param name="versionName"></param>
    /// <returns></returns>
    public async Task<Session?> GetBySessionIdVersionNameAsync(Guid sessionId, string versionName)
    {
        return await _context.Sessions
            .Include(s => s.SessionVersions)
            .Where(s => s.SessionId == sessionId)
            .SingleOrDefaultAsync();
    }
}
