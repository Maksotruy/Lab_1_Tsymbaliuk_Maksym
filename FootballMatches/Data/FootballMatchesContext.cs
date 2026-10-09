
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

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<FootballMatch>()
            .HasIndex(x => new
            {
                x.HomeTeam,
                x.AwayTeam,
                x.MatchDate
            })
            .IsUnique();
    }
}
