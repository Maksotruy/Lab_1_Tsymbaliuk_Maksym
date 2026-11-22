
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Scalar.AspNetCore;

using FootballMatches.Api;
using FootballMatches.Components;
using FootballMatches.Data;
using FootballMatches.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// Налаштування Blazor
// =====================================

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// =====================================
// ЛР №13: Статичні ресурси в Production
// =====================================

// Дозволяє перевіряти Production через dotnet run,
// коли застосунок ще не опубліковано.
if (builder.Environment.IsProduction())
{
    builder.WebHost.UseStaticWebAssets();
}

// =====================================
// ЛР №13: Конфігурація SQLite
// =====================================

var connectionString = builder.Configuration
    .GetConnectionString("Default")
    ?? throw new InvalidOperationException(
        "Не знайдено рядок підключення ConnectionStrings:Default.");

builder.Services.AddDbContextFactory<FootballMatchesContext>(options =>
    options.UseSqlite(connectionString));

// QuickGrid
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Обробка помилок бази даних
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ЛР №11: OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// =====================================
// ЛР №13: Автоматичні міграції
// =====================================

var factory = app.Services
    .GetRequiredService<IDbContextFactory<FootballMatchesContext>>();

await using (var db = await factory.CreateDbContextAsync())
{
    await db.Database.MigrateAsync();
}

// =====================================
// OpenAPI та Scalar
// =====================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// =====================================
// Обробка помилок
// =====================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Error",
        createScopeForErrors: true);

    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true);

// HTTPS-перенаправлення лише в Development,
// щоб не заважати запуску контейнера через HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

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
