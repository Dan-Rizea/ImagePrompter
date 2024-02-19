namespace Application.Dtos
{
    /// <summary>
    /// This is used as a data transfer object between ChatGPT and the mailing functionality
    /// </summary>
    internal class MailingDto
    {
        public string Email { get; set; }
        public string? Subject { get; set; }
        public string? MessageBody { get; set; }
        public string? CustomerName { get; set; }
        public bool Error { get; set; } = false;    
    }
}
