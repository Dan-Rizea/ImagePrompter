namespace Application.Models
{
    internal class MailingModel
    {
        public string Email { get; set; }
        public string? Subject { get; set; }
        public string? MessageBody { get; set; }
        public string? CustomerName { get; set; }
        public bool Error { get; set; } = false;    
    }
}
