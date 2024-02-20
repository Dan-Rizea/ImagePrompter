using Application.Dtos;

namespace Application.Services.SecretManager
{
    public interface ISecretManager
    {
        public Task<MailingCredentialsDto> GetMailingCredentials();
        public Task<string> GetOpenAIApiKey();
        public Task<string> GetStabilityAIApiKey();
    }
}
