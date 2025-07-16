using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApp.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.Property(a => a.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(a => a.ProfilePictureUrl)
              .HasMaxLength(500);

        entity.HasMany(a => a.Characters)
              .WithOne(c => c.User)
              .HasForeignKey(c => c.UserId);
    }
}