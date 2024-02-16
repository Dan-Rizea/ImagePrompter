using Persistence.Entities;

namespace Application.Services
{
    public interface ISessionLogicService
    {
        public Task<Session> InitializeSessionAsync();
    }
}
