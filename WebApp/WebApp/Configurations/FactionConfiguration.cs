using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Configurations;

public class FactionConfiguration : IEntityTypeConfiguration<Faction>
{
    public void Configure(EntityTypeBuilder<Faction> entity)
    {
        entity.HasKey(f => f.Id);

        entity.Property(f => f.Name)
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(f => f.Description)
              .HasMaxLength(500);

        entity.HasMany(f => f.Characters)
              .WithOne(c => c.Faction)
              .HasForeignKey(c => c.FactionId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}