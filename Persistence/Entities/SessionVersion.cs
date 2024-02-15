namespace Persistence.Entities
{
    /// <summary>
    /// Session versions are used to ensure that no data is lost within a session
    /// </summary>
    public class SessionVersion
    {
        public int Id { get; set; }    
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string? Prompt { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
