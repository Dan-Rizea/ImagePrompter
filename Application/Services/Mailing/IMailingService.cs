using Application.Dtos;

namespace Application.Services.Mailing
{
    public interface IMailingService
    {
        /// <summary>
        /// Uses mailkit to send e-mails
        /// </summary>
        /// <param name="attachments">Image attachment</param>
        /// <param name="mailingDetails">Customer name</param>
        public Task SendMailAsync(byte[] attachments, MailingDto mailingDetails); 
    }
}
