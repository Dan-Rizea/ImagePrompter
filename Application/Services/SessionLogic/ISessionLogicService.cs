using Persistence.Entities;

namespace Application.Services.SessionLogic
{
    public interface ISessionLogicService
    {
        public Task<Session> InitializeSessionAsync();
    }
}
