using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;
using DentalApp.Pages;

namespace DentalApp.ViewModels;

public partial class PatientsViewModel : ObservableObject
{
    private readonly IPatientRepository _repo;

    public PatientsViewModel(IPatientRepository repo)
    {
        _repo = repo;
    }

    [ObservableProperty]
    private ObservableCollection<Patient> patients = new();

    [RelayCommand]
    private async Task Load()
    {
        var data = await _repo.GetAllAsync();
        Patients = new ObservableCollection<Patient>(data);
    }

    [RelayCommand]
    private async Task AddPatient()
    {
        SentrySdk.Logger.LogInfo("A simple log message");
        SentrySdk.Logger.LogError("A {0} log message", "formatted");
        await Shell.Current.GoToAsync(nameof(AddPatientPage));
    }
    
    [RelayCommand]
    private async Task OpenPatient(Patient patient)
    {
        if (patient == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(PatientDetailsPage)}?patientId={patient.Id}");
    }
}