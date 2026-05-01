using DentalApp.ViewModels;

namespace DentalApp.Pages;

public partial class PatientDetailsPage : ContentPage
{
    public PatientDetailsPage(PatientDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}