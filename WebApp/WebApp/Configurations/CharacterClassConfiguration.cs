using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Models;

namespace WebApp.Configurations;

public class CharacterClassConfiguration : IEntityTypeConfiguration<CharacterClass>
{
    public void Configure(EntityTypeBuilder<CharacterClass> entity)
    {
        entity.HasKey(c => c.Id);

        entity.Property(c => c.Name)
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(c => c.Description)
              .HasMaxLength(500);

        entity.HasMany(c => c.Characters)
              .WithOne(ch => ch.CharacterClass)
              .HasForeignKey(ch => ch.CharacterClassId);
    }
}