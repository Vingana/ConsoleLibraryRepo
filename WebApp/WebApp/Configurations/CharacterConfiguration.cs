using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Configurations;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> entity)
    {
        entity.HasKey(c => c.Id);

        entity.Property(c => c.Name)
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(c => c.Description)
              .HasMaxLength(1000);

        entity.HasOne(c => c.User)
              .WithMany(a => a.Characters)
              .HasForeignKey(c => c.UserId)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(c => c.CharacterClass)
              .WithMany()
              .HasForeignKey(c => c.CharacterClassId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(c => c.Race)
              .WithMany()
              .HasForeignKey(c => c.RaceId)
              .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(c => c.Faction)
              .WithMany(f => f.Characters)
              .HasForeignKey(c => c.FactionId)
              .OnDelete(DeleteBehavior.Restrict);
    }
}