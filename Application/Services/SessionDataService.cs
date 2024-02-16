using Persistence.Entities;

namespace Application.Services
{
    public class SessionDataService
    {
        public Session CurrentSession { get; set; }
        public string CurrentSessionVersionName { get; set; }
    }
}
