using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Configurations;

public class RaceConfiguration : IEntityTypeConfiguration<Race>
{
    public void Configure(EntityTypeBuilder<Race> entity)
    {
        entity.HasKey(r => r.Id);

        entity.Property(r => r.Name)
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(r => r.Description)
              .HasMaxLength(500);

        entity.HasMany(r => r.Characters)
              .WithOne(c => c.Race)
              .HasForeignKey(c => c.RaceId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}