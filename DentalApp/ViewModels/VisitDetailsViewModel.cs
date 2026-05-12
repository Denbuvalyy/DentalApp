using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;
using DentalApp.Pages;
using Microsoft.Extensions.Logging;

namespace DentalApp.ViewModels;

public partial class VisitDetailsViewModel : ObservableObject
{
    private readonly IVisitPhotoRepository _photoRepo;
    private readonly INavigationService _navigationService;
    private readonly ILogger<VisitDetailsViewModel> _logger;

    public VisitDetailsViewModel(
        IVisitPhotoRepository photoRepo,
        INavigationService navigationService,
        ILogger<VisitDetailsViewModel> logger)
    {
        _photoRepo = photoRepo;
        _navigationService = navigationService;
        _logger = logger;
    }

    [ObservableProperty]
    private ObservableCollection<VisitPhotoItemViewModel> _photos = new();

    private int visitId;

    public async Task Init(int id)
    {
        visitId = id;

        var data = await _photoRepo.GetByVisitIdAsync(id);

        Photos = new ObservableCollection<VisitPhotoItemViewModel>(
            data.Select(x => new VisitPhotoItemViewModel(x)));
    }

    // 🔍 fullscreen
    [RelayCommand]
    private async Task OpenPhoto(VisitPhotoItemViewModel photo)
    {
        var allPhotos = Photos
            .Select(x => x.Model)
            .ToList();

        var index = Photos.IndexOf(photo);

        await _navigationService.GoToGallery(allPhotos, index);
        //await _navigationService.GoToPhoto(photo.Model);
    }

    // 🗑 delete
    [RelayCommand]
    private async Task DeletePhoto(VisitPhotoItemViewModel photo)
    {
        if (photo == null)
            return;

        try
        {
            if (File.Exists(photo.FilePath))
            {
                File.Delete(photo.FilePath);
            }
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Failed to delete photo file");
        }

        await _photoRepo.DeleteAsync(photo.Model);

        Photos.Remove(photo);
    }
}