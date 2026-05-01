using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;
using DentalApp.Pages;

namespace DentalApp.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientDetailsViewModel : ObservableObject
{
    private readonly IPatientRepository _patientRepo;
    private readonly IVisitRepository _visitRepo;
    private readonly INavigationService _navigationService;

    public PatientDetailsViewModel(
        IPatientRepository patientRepo,
        IVisitRepository visitRepo,
        INavigationService navigationService)
    {
        _patientRepo = patientRepo;
        _visitRepo = visitRepo;
        _navigationService = navigationService;
    }

    private int patientId;

    public string PatientId
    {
        set
        {
            if (int.TryParse(value, out var id))
            {
                patientId = id;
                _ = Load(id);
            }
        }
    }

    [ObservableProperty]
    private Patient patient;

    [ObservableProperty]
    private ObservableCollection<Visit> visits = new();

    private async Task Load(int id)
    {
        var patient = await _patientRepo.GetAsync(id);

        if (patient == null)
            return;

        Patient = patient;

        var data = await _visitRepo.GetByPatientIdAsync(id);
        Visits = new ObservableCollection<Visit>(data);
    }

    [RelayCommand]
    private async Task AddVisit()
    {
        var visit = await _navigationService.GoToAddVisit(patientId);

        if (visit != null)
            Visits.Add(visit);
        //await _navigationService.GoToAddVisit(patientId);
        //await Shell.Current.GoToAsync($"{nameof(AddVisitPage)}?patientId={patientId}");
    }
    
    [RelayCommand]
    private async Task OpenVisit(Visit visit)
    {
        await _navigationService.GoToVisitDetails(visit.Id);
    }
    
    
}