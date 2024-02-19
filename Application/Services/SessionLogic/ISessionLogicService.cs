using Persistence.Entities;

namespace Application.Services.SessionLogic
{
    public interface ISessionLogicService
    {
        /// <summary>
        /// Initializes and stores a new session version whenever a user generates an image
        /// </summary>
        /// <param name="sessionVersionName">The name of the version of the session</param>
        /// <param name="prompt">User-provided prompt</param>
        /// <param name="image">The generated image</param>
        /// <param name="currentSession">The current user session</param>
        /// <returns>A new session version</returns>
        public Task<SessionVersion> InitializeNewSessionVersionAsync(string sessionVersionName, string prompt, byte[] image, Session currentSession);
        
        /// <summary>
        /// Initializes a new session whenever a user accesses the website
        /// </summary>
        /// <param name="enableNavigation">States whether the user should be redirected to the new session</param>
        /// <returns>A new session</returns>
        public Task<Session> InitializeSessionAsync(bool enableNavigation = true);
    }
}
