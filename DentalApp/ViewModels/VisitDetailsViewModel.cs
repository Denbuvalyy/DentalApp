using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;
using DentalApp.Pages;

namespace DentalApp.ViewModels;

public partial class VisitDetailsViewModel : ObservableObject
{
    private readonly IVisitPhotoRepository _photoRepo;
    private readonly INavigationService _navigationService;

    public VisitDetailsViewModel(IVisitPhotoRepository photoRepo,  INavigationService navigationService)
    {
        _photoRepo = photoRepo;
        _navigationService = navigationService;
    }

    [ObservableProperty]
    private ObservableCollection<VisitPhoto> _photos = new();

    private int visitId;

    public async Task Init(int id)
    {
        visitId = id;

        var data = await _photoRepo.GetByVisitIdAsync(id);
        Photos = new ObservableCollection<VisitPhoto>(data);
    }

    // 🔍 fullscreen
    [RelayCommand]
    private async Task OpenPhoto(VisitPhoto photo)
    {
        await _navigationService.GoToPhoto(photo);
        // await Shell.Current.GoToAsync(nameof(PhotoPage),
        //     new Dictionary<string, object>
        //     {
        //         ["photo"] = photo
        //     });
    }

    // 🗑 delete
    [RelayCommand]
    private async Task DeletePhoto(VisitPhoto photo)
    {
        if (photo == null)
            return;

        File.Delete(photo.FilePath);

        await _photoRepo.DeleteAsync(photo);

        Photos.Remove(photo);
    }
}