namespace Application.Services.Mailing
{
    public interface IMailingService
    {
        /// <summary>
        /// Uses mailkit to send e-mails
        /// </summary>
        /// <param name="attachments">Image attachment</param>
        /// <param name="email">Email address</param>
        /// <param name="subject">Subject</param>
        /// <param name="messageBody">Message body</param>
        /// <param name="customerName">Customer name</param>
        public Task SendMailAsync(byte[] attachments, 
            string email, 
            string subject = "Here is your image", 
            string messageBody = "Thank you for using our service!", 
            string customerName = "Beloved Customer");
    }
}
