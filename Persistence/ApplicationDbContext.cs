using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionVersion> Versions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Soft deletable entities
            modelBuilder.Entity<Session>().HasQueryFilter(e => e.DeletedAt == default);
        }
    }
}
