using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.EntityFrameworkConfigurations
{
    internal class SessionVersionConfiguration : IEntityTypeConfiguration<SessionVersion>
    {
        public void Configure(EntityTypeBuilder<SessionVersion> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(128).IsRequired();

            builder.Property(e => e.Image).IsRequired();

            builder.Property(e => e.Prompt).HasMaxLength(10000).IsRequired();

            builder.HasIndex(e => e.SessionId).IsUnique(false);
            builder.HasOne(e => e.Session).WithMany(e => e.SessionVersions).HasForeignKey(e => e.SessionId);
        }
    }
}
