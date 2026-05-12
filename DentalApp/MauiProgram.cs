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
            .UseMauiApp<App>();

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