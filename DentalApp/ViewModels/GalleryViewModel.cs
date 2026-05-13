using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalApp.Core.Models;

namespace DentalApp.ViewModels;

public partial class GalleryViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<GalleryPhotoItemViewModel> photos = new();

    [ObservableProperty]
    private int currentIndex;
    
    [ObservableProperty]
    private bool _isSwipeEnabled = true;

    public void Init(List<VisitPhoto> photos, int startIndex)
    {
        Photos = new ObservableCollection<GalleryPhotoItemViewModel>(
            photos.Select(x => new GalleryPhotoItemViewModel(x)));
        foreach (var photo in Photos)
        {
            photo.PropertyChanged += PhotoChanged;
        }

        CurrentIndex = startIndex;
    }

    [RelayCommand]
    private async Task Close()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    private void PhotoChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(GalleryPhotoItemViewModel.IsZoomed))
        {
            IsSwipeEnabled = !Photos.Any(x => x.IsZoomed);
        }
    }
}