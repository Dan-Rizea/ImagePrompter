namespace Persistence.Entities
{
    /// <summary>
    /// A session is used to log and interact with the trail of modified images.
    /// </summary>
    public class Session : SoftDeletableEntity
    {
        public int Id { get; set; }
        public Guid SessionId { get; set; }

        public int SessionVersionId { get; set; }
        public IEnumerable<SessionVersion> SessionVersions { get; set; }
    }
}
