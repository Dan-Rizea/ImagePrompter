using Application.Dtos;
using Google.Cloud.SecretManager.V1;
using Newtonsoft.Json;

namespace Application.Services.SecretManager
{
    public class SecretManager : ISecretManager
    {
        public async Task<MailingCredentialsDto> GetMailingCredentials()
        {
            try
            {
                string gcpProjectId = "imageprompter-414819";
                string secretName = "MailingCredentials";
                string secretVersion = "1";

                SecretManagerServiceClient client = SecretManagerServiceClient.Create();
                SecretVersionName secretVersionName = new SecretVersionName(gcpProjectId, secretName, secretVersion);

                var response = await client.AccessSecretVersionAsync(secretVersionName);
                var secret = response.Payload.Data.ToStringUtf8();
                var deserializedSecret = JsonConvert.DeserializeObject<MailingCredentialsDto>(secret);

                return deserializedSecret;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> GetOpenAIApiKey()
        {
            try
            {
                string gcpProjectId = "imageprompter-414819";
                string secretName = "OpenAIAPIKey";
                string secretVersion = "1";

                SecretManagerServiceClient client = SecretManagerServiceClient.Create();
                SecretVersionName secretVersionName = new SecretVersionName(gcpProjectId, secretName, secretVersion);

                var response = await client.AccessSecretVersionAsync(secretVersionName);
                var secret = response.Payload.Data.ToStringUtf8();

                return secret;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> GetStabilityAIApiKey()
        {
            try
            {
                string gcpProjectId = "imageprompter-414819";
                string secretName = "StabilityAIAPIKey";
                string secretVersion = "1";

                SecretManagerServiceClient client = SecretManagerServiceClient.Create();
                SecretVersionName secretVersionName = new SecretVersionName(gcpProjectId, secretName, secretVersion);

                var response = await client.AccessSecretVersionAsync(secretVersionName);
                var secret = response.Payload.Data.ToStringUtf8();

                return secret;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

    }
}
