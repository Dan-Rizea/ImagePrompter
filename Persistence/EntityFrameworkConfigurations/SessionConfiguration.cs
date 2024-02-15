using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityFrameworkConfigurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.SessionId).IsRequired();

            builder.HasIndex(e => e.SessionVersionId).IsUnique(false);
            builder.HasMany(e => e.SessionVersions).WithOne(e => e.Session);

            builder.HasIndex(e => e.DeletedAt).IsUnique(false);
        }
    }
}
