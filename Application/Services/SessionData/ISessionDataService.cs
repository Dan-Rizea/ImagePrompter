using Persistence.Entities;

namespace Application.Services.SessionData
{
    public interface ISessionDataService
    {
        public Session CurrentSession { get; set; }
        public SessionVersion CurrentSessionVersion { get; set; }
    }
}
