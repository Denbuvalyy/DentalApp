using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalApp.Core.Models;

namespace DentalApp.ViewModels;

public partial class PhotoViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Image))]
    private VisitPhoto? photo;

    public void Init(VisitPhoto photo)
    {
        Photo = photo;
    }

    public ImageSource? Image =>
        string.IsNullOrWhiteSpace(Photo?.FilePath)
            ? null
            : ImageSource.FromFile(Photo.FilePath);

    [RelayCommand]
    private async Task Close()
    {
        await Shell.Current.GoToAsync("..");
    }
}