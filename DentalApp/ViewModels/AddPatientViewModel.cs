using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;

namespace DentalApp.ViewModels;

public partial class AddPatientViewModel : ObservableObject
{
    private readonly IPatientRepository _repo;

    public AddPatientViewModel(IPatientRepository repo)
    {
        _repo = repo;
    }

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string phone;

    [ObservableProperty]
    private string notes;

    private bool CanSave => !string.IsNullOrWhiteSpace(Name);

    partial void OnNameChanged(string value)
    {
        SaveCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task Save()
    {
        var patient = new Patient
        {
            Name = Name,
            Phone = Phone,
            Notes = Notes
        };

        await _repo.AddAsync(patient);

        await Shell.Current.GoToAsync("..");
    }
}