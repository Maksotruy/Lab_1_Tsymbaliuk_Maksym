
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Scalar.AspNetCore;

using FootballMatches.Api;
using FootballMatches.Components;
using FootballMatches.Data;
using FootballMatches.Services;

var builder = WebApplication.CreateBuilder(args);

// Налаштування Blazor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Підключення SQLite
builder.Services.AddDbContextFactory<FootballMatchesContext>(options =>
    options.UseSqlite("Data Source=footballmatches.db"));

// QuickGrid
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Обробка помилок бази даних
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ЛР №11: OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Документація OpenAPI та Scalar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Обробка помилок у Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

// =====================================
// ЛР №11: WEB API
// =====================================

var api = app.MapGroup("/api")
    .WithTags("FootballMatches");

// 1. Отримання всіх футбольних матчів
api.MapGet("/footballmatches",
    async Task<Ok<List<FootballMatchDto>>> (
        IDbContextFactory<FootballMatchesContext> factory) =>
    {
        await using var db =
            await factory.CreateDbContextAsync();

        var matches = await db.FootballMatches
            .AsNoTracking()
            .OrderByDescending(m => m.MatchDate)
            .Select(m => new
            {
                m.Id,
                m.HomeTeam,
                m.AwayTeam,
                m.HomeScore,
                m.AwayScore,
                m.MatchDate,
                m.Stadium,
                m.Status,
                m.Competition
            })
            .ToListAsync();

        var result = matches.Select(m =>
            new FootballMatchDto(
                m.Id,
                m.HomeTeam,
                m.AwayTeam,
                m.HomeScore,
                m.AwayScore,
                m.MatchDate,
                m.Stadium,
                m.Status.ToString(),
                m.Competition
            )).ToList();

        return TypedResults.Ok(result);
    })
    .WithName("GetFootballMatches")
    .WithSummary("Отримати список футбольних матчів");

// 2. Отримання одного матчу за ID
api.MapGet("/footballmatches/{id:int}",
    async Task<Results<Ok<FootballMatchDto>, NotFound>> (
        int id,
        IDbContextFactory<FootballMatchesContext> factory) =>
    {
        await using var db =
            await factory.CreateDbContextAsync();

        var match = await db.FootballMatches
            .AsNoTracking()
            .Where(m => m.Id == id)
            .Select(m => new
            {
                m.Id,
                m.HomeTeam,
                m.AwayTeam,
                m.HomeScore,
                m.AwayScore,
                m.MatchDate,
                m.Stadium,
                m.Status,
                m.Competition
            })
            .FirstOrDefaultAsync();

        if (match is null)
        {
            return TypedResults.NotFound();
        }

        var result = new FootballMatchDto(
            match.Id,
            match.HomeTeam,
            match.AwayTeam,
            match.HomeScore,
            match.AwayScore,
            match.MatchDate,
            match.Stadium,
            match.Status.ToString(),
            match.Competition
        );

        return TypedResults.Ok(result);
    })
    .WithName("GetFootballMatchById")
    .WithSummary("Отримати футбольний матч за ідентифікатором");

// 3. Отримання статистики за командами
api.MapGet("/summary",
    async Task<Ok<List<SummaryRow>>> (
        IDbContextFactory<FootballMatchesContext> factory) =>
    {
        await using var db =
            await factory.CreateDbContextAsync();

        var summary = await SummaryCalculator.ByTeamAsync(
            db.FootballMatches.AsNoTracking());

        return TypedResults.Ok(summary);
    })
    .WithName("GetFootballSummary")
    .WithSummary("Отримати статистику футбольних матчів за командами");

// =====================================
// Маршрути Blazor
// =====================================

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
