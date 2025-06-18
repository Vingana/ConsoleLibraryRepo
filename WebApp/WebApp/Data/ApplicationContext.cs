using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Configurations;
using WebApp.Models;

namespace WebApp.Data;

public sealed class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Character> Characters => Set<Character>();
    public DbSet<CharacterClass> CharacterClasses => Set<CharacterClass>();
    public DbSet<Faction> Factions => Set<Faction>();
    public DbSet<Race> Races => Set<Race>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CharacterConfiguration());
        modelBuilder.ApplyConfiguration(new CharacterClassConfiguration());
        modelBuilder.ApplyConfiguration(new FactionConfiguration());
        modelBuilder.ApplyConfiguration(new RaceConfiguration());
    }
}
