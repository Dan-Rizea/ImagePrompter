namespace Application.Services.Mailing
{
    public interface IMailingService
    {
        public Task SendMailAsync(byte[] attachments, 
            string email, 
            string subject = "Here is your image", 
            string messageBody = "Thank you for using our service!", 
            string customerName = "Beloved Customer");
    }
}
