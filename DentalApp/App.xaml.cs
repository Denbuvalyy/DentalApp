using DentalApp.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DentalApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        using var scope = IPlatformApplication.Current.Services.CreateScope();
        var db = scope?.ServiceProvider?.GetRequiredService<AppDbContext>();
        db?.Database?.EnsureCreated();
        
        //MainPage = new NavigationPage(new AppShell());
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}