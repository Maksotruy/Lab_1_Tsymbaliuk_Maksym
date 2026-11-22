
using Microsoft.EntityFrameworkCore;
using FootballMatches.Models;

namespace FootballMatches.Services;

// Рядок зведеної таблиці
public record SummaryRow(
    string Name,
    int Count,
    int Total
);

public static class SummaryCalculator
{
    // 1. Групування за домашніми командами
    public static async Task<List<SummaryRow>> ByTeamAsync(
        IQueryable<FootballMatch> query)
    {
        var raw = await query
            .GroupBy(m => m.HomeTeamReference!.Name)
            .Select(g => new
            {
                Name = g.Key,
                Count = g.Count(),
                Total = g.Sum(m => m.HomeScore)
            })
            .OrderByDescending(r => r.Total)
            .ToListAsync();

        return raw
            .Select(r => new SummaryRow(
                r.Name,
                r.Count,
                r.Total
            ))
            .ToList();
    }

    // 2. Групування за роком і місяцем
    public static async Task<List<SummaryRow>> ByMonthAsync(
        IQueryable<FootballMatch> query)
    {
        var raw = await query
            .GroupBy(m => new
            {
                m.MatchDate.Year,
                m.MatchDate.Month
            })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                Count = g.Count(),
                Total = g.Sum(m => m.HomeScore)
            })
            .ToListAsync();

        return raw
            .OrderBy(r => r.Year)
            .ThenBy(r => r.Month)
            .Select(r => new SummaryRow(
                $"{r.Month:D2}.{r.Year}",
                r.Count,
                r.Total
            ))
            .ToList();
    }

    // 3. Загальний підсумок
    public static async Task<SummaryRow> TotalAsync(
        IQueryable<FootballMatch> query)
    {
        return new SummaryRow(
            "Усього",
            await query.CountAsync(),
            await query.SumAsync(m => m.HomeScore)
        );
    }
}
