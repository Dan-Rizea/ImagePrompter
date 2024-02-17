using Persistence.Entities;

namespace Application.Services.SessionData
{
    public class SessionDataService : ISessionDataService
    {
        public Session CurrentSession { get; set; }
        public SessionVersion CurrentSessionVersion { get; set; }
    }
}
