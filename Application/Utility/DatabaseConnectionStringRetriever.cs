using Google.Cloud.SecretManager.V1;

namespace Application.Utility
{
    public static class DatabaseConnectionStringRetriever
    {
        public static async Task<string> GetDatabaseCredentials()
        {
            try
            {
                string gcpProjectId = "imageprompter-414819";
                string secretName = "SQLServerConnectionStringPROD";
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
