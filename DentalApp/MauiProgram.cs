using DentalApp.Core.Interfaces;
using DentalApp.Infrastructure.Data;
using DentalApp.Infrastructure.Repositories;
using DentalApp.Pages;
using DentalApp.Services;
using DentalApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DentalApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseSentry(options =>
            {
                // The DSN is the only required setting.
                options.Dsn =
                    "https://ddf2a6eed53e3a6c290f7e990ce6a783@o4511397946982400.ingest.de.sentry.io/4511398044631120";

                // Use debug mode if you want to see what the SDK is doing.
                // Debug messages are written to stdout with Console.Writeline,
                // and are viewable in your IDE's debug console or with 'adb logcat', etc.
                // This option is not recommended when deploying your application.
                options.Debug = true;

                // Set TracesSampleRate to 1.0 to capture 100% of transactions for tracing.
                // We recommend adjusting this value in production.
                options.TracesSampleRate = 1.0;
                // Enable logs to be sent to Sentry
                options.EnableLogs = true;
                options.Release =
                    $"{AppInfo.VersionString} ({AppInfo.BuildString})";
#if DEBUG
                options.Environment = "development";
#else
    options.Environment = "production";
#endif
                // Other Sentry options can be set here.
            });

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Filename={dbPath}"));
        //builder.Services.AddSingleton(new AppDbContext(dbPath));
        
        builder.Services.AddSingleton<IPatientRepository, PatientRepository>();
        
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddSingleton<INavigationService>(sp =>
            sp.GetRequiredService<NavigationService>());
        builder.Services.AddSingleton<INavigationResult>(sp =>
            sp.GetRequiredService<NavigationService>());
        //builder.Services.AddSingleton<INavigationService, NavigationService>();

        builder.Services.AddTransient<PatientsViewModel>();
        builder.Services.AddTransient<PatientsPage>();
        builder.Services.AddTransient<AddPatientViewModel>();
        builder.Services.AddTransient<AddPatientPage>();
        builder.Services.AddTransient<IVisitRepository, VisitRepository>();
        builder.Services.AddTransient<PatientDetailsViewModel>();
        builder.Services.AddTransient<PatientDetailsPage>();
        builder.Services.AddTransient<AddVisitViewModel>();
        builder.Services.AddTransient<AddVisitPage>();
        builder.Services.AddTransient<IVisitPhotoRepository, VisitPhotoRepository>();
        builder.Services.AddTransient<VisitDetailsViewModel>();
        builder.Services.AddTransient<VisitDetailsPage>();
        builder.Services.AddTransient<PhotoViewModel>();
        builder.Services.AddTransient<GalleryPage>();
        builder.Services.AddTransient<GalleryViewModel>();

        // 🔥 1. Створюємо app
        var app = builder.Build();

        // 🔥 2. Ініціалізація БД
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }
        
        // 🔥 3. Повертаємо app
        return app;
    }
}