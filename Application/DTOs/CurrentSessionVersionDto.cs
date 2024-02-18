namespace Application.DTOs
{
    public class CurrentSessionVersionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string? Prompt { get; set; }

        public int SessionId { get; set; }
    }
}
