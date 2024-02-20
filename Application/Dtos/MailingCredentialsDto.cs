namespace Application.Dtos
{
    public record MailingCredentialsDto
    {
        public string SenderName { get; init; }
        public string SenderEmail { get; init; }
        public string Server { get; init; }
        public string Port { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}