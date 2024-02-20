using Application.Dtos;
using Application.Services.SecretManager;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Application.Services.Mailing
{
    public class MailingService : IMailingService
    {
        private readonly ISecretManager _secretManager;

        public MailingService(ISecretManager secretManager)
        {
            _secretManager = secretManager;
        }
        
        /// <inheritdoc cref="IMailingService.SendMailAsync(byte[], MailingDto)"/>
        public async Task SendMailAsync(byte[] attachments, MailingDto mailingDetails)
        {
            var mailingCredentials = await _secretManager.GetMailingCredentials();

            //Mailing configuration
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailingCredentials.SenderName, mailingCredentials.SenderEmail));
            message.To.Add(new MailboxAddress(mailingDetails.CustomerName, mailingDetails.Email));
            message.Subject = mailingDetails.Subject;

            var builder = new BodyBuilder();
            builder.TextBody = mailingDetails.MessageBody;
            builder.Attachments.Add(@"Output.png", attachments);

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(mailingCredentials.Server, int.Parse(mailingCredentials.Port), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(mailingCredentials.UserName, mailingCredentials.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
