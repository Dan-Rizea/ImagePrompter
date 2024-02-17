using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Application.Services.Mailing
{
    public class MailingService : IMailingService
    {
        private readonly IConfiguration _config;

        public MailingService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMailAsync(byte[] attachments, string email, string subject, string messageBody, string customerName)
        {
            var senderName = _config["MailSettings:SenderName"];
            var senderEmail = _config["MailSettings:SenderEmail"];
            var server = _config["MailSettings:Server"];
            var port = int.Parse(_config["MailSettings:Port"]);
            var username = _config["MailSettings:UserName"];
            var password = _config["MailSettings:Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(customerName, email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.TextBody = messageBody;
            builder.Attachments.Add(@"Output.png", attachments);

            message.Body = builder.ToMessageBody();

            //TODO: Address security issues: not using a SecureString for authentication does not seem right to me.
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(server, port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(username, password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
