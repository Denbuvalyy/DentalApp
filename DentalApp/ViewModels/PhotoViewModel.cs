using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalApp.Core.Models;

namespace DentalApp.ViewModels;

public partial class PhotoViewModel : ObservableObject
{
    [ObservableProperty]
    private VisitPhoto photo;

    [RelayCommand]
    private async Task Close()
    {
        await Shell.Current.GoToAsync("..");
    }
}