namespace Persistence.Entities
{
    /// <summary>
    /// Used for recovery/auditing reasons
    /// </summary>
    public abstract class SoftDeletableEntity
    {
        public DateTime? DeletedAt { get; set; }
    }
}
