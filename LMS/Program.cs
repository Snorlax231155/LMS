using LMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register EF Core
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Auto-create migrations (if missing) and apply EF Core migrations at startup (development convenience)
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        // In development, if there are no migration files, scaffold one automatically using the dotnet-ef tool.
        if (app.Environment.IsDevelopment())
        {
            var migrationsDir = Path.Combine(app.Environment.ContentRootPath ?? ".", "Migrations");
            var hasMigrationFiles = Directory.Exists(migrationsDir) && Directory.EnumerateFiles(migrationsDir, "*.cs").Any();
            if (!hasMigrationFiles)
            {
                try
                {
                    // Create a timestamped migration name
                    var migrationName = "AutoSeedBooks_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
                    var startInfo = new ProcessStartInfo("dotnet")
                    {
                        Arguments = $"ef migrations add {migrationName} -p LMS -s LMS",
                        WorkingDirectory = app.Environment.ContentRootPath,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    logger.LogInformation("Scaffolding migration {MigrationName}...", migrationName);
                    using var proc = Process.Start(startInfo);
                    if (proc != null)
                    {
                        var outText = proc.StandardOutput.ReadToEnd();
                        var errText = proc.StandardError.ReadToEnd();
                        proc.WaitForExit();
                        logger.LogInformation(outText);
                        if (!string.IsNullOrEmpty(errText)) logger.LogWarning(errText);
                    }
                    else
                    {
                        logger.LogWarning("Could not start dotnet process to scaffold migration.");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Failed to scaffold migration automatically. You can run 'dotnet ef migrations add <Name> -p LMS -s LMS' manually.");
                }
            }
        }

        var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
        db.Database.Migrate();

        // After applying migrations, attempt to run database update via dotnet ef if in development and migrations were just added
        if (app.Environment.IsDevelopment())
        {
            try
            {
                var startInfo2 = new ProcessStartInfo("dotnet")
                {
                    Arguments = "ef database update -p LMS -s LMS",
                    WorkingDirectory = app.Environment.ContentRootPath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                logger.LogInformation("Running 'dotnet ef database update'...");
                using var proc2 = Process.Start(startInfo2);
                if (proc2 != null)
                {
                    var outText2 = proc2.StandardOutput.ReadToEnd();
                    var errText2 = proc2.StandardError.ReadToEnd();
                    proc2.WaitForExit();
                    logger.LogInformation(outText2);
                    if (!string.IsNullOrEmpty(errText2)) logger.LogWarning(errText2);
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to run 'dotnet ef database update' automatically.");
            }
        }
    }
    catch (Exception ex)
    {
        // Log and continue; migration failures should be visible in the console/logs
        logger.LogError(ex, "An error occurred while creating/applying the database migrations.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
