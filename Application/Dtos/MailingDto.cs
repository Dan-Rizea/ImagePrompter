namespace Application.Dtos
{
    /// <summary>
    /// This is used as a data transfer object between ChatGPT and the mailing functionality
    /// </summary>
    public record MailingDto
    {
        public string Email { get; init; }
        public string? Subject { get; init; }
        public string? MessageBody { get; init; }
        public string? CustomerName { get; init; }
        public bool Error { get; init; } = false;    
    }
}
