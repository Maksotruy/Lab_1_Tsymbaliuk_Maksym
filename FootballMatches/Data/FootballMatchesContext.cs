
using Microsoft.EntityFrameworkCore;
using FootballMatches.Models;

namespace FootballMatches.Data;

public class FootballMatchesContext : DbContext
{
    public FootballMatchesContext(
        DbContextOptions<FootballMatchesContext> options)
        : base(options)
    {
    }

    public DbSet<FootballMatch> FootballMatches
        => Set<FootballMatch>();

    public DbSet<Team> Team
        => Set<Team>();

    public DbSet<Stadium> Stadium
        => Set<Stadium>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // Унікальність футбольного матчу
        b.Entity<FootballMatch>()
            .HasIndex(x => new
            {
                x.HomeTeam,
                x.AwayTeam,
                x.MatchDate
            })
            .IsUnique();

        // Зв'язок із домашньою командою
        b.Entity<FootballMatch>()
            .HasOne(x => x.HomeTeamReference)
            .WithMany()
            .HasForeignKey(x => x.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        // Зв'язок із гостьовою командою
        b.Entity<FootballMatch>()
            .HasOne(x => x.AwayTeamReference)
            .WithMany()
            .HasForeignKey(x => x.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        // Зв'язок зі стадіоном
        b.Entity<FootballMatch>()
            .HasOne(x => x.StadiumReference)
            .WithMany()
            .HasForeignKey(x => x.StadiumId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
