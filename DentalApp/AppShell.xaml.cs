using DentalApp.Pages;

namespace DentalApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(AddPatientPage), typeof(AddPatientPage));
        Routing.RegisterRoute(nameof(PatientDetailsPage), typeof(PatientDetailsPage));
        Routing.RegisterRoute(nameof(AddVisitPage), typeof(AddVisitPage));
        Routing.RegisterRoute(nameof(PhotoPage), typeof(PhotoPage));
    }
}