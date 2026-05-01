using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace DentalApp.ViewModels;

public partial class AddVisitViewModel : ObservableObject
{
    private readonly IVisitRepository _visitRepo;
    private readonly IVisitPhotoRepository _photoRepo;
    private readonly INavigationService _navigationService;
    private readonly INavigationResult _navigationResult;

    private int patientId;

    public AddVisitViewModel(
        IVisitRepository visitRepo,
        IVisitPhotoRepository photoRepo,
        INavigationService navigationService,
        INavigationResult navigationResult)
    {
        _visitRepo = visitRepo;
        _photoRepo = photoRepo;
        _navigationService = navigationService;
        _navigationResult = navigationResult;

        Date = DateTime.Now;
    }

    public void Init(int id)
    {
        patientId = id;
    }

    [ObservableProperty]
    private DateTime date;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private ObservableCollection<VisitPhoto> photos = new();

    private int visitId; // після save

    // 📸 зробити фото
    [RelayCommand]
    private async Task TakePhoto()
    {
        var result = await MediaPicker.CapturePhotoAsync();

        if (result == null)
            return;

        var path = await SaveFile(result);

        Photos.Add(new VisitPhoto
        {
            FilePath = path,
            CreatedAt = DateTime.Now
        });
    }

    // 🖼 вибрати з галереї
    [RelayCommand]
    private async Task PickPhoto()
    {
        var result = await MediaPicker.PickPhotoAsync();

        if (result == null)
            return;

        var path = await SaveFile(result);

        Photos.Add(new VisitPhoto
        {
            FilePath = path,
            CreatedAt = DateTime.Now
        });
    }

    private async Task<string> SaveFile(FileResult file)
    {
        var fileName = $"{Guid.NewGuid()}.jpg";
        var newPath = Path.Combine(FileSystem.AppDataDirectory, "photos");

        Directory.CreateDirectory(newPath);

        var fullPath = Path.Combine(newPath, fileName);

        using var stream = await file.OpenReadAsync();
        using var newStream = File.OpenWrite(fullPath);

        await stream.CopyToAsync(newStream);

        return fullPath;
    }

    [RelayCommand]
    private async Task Save()
    {
        var visit = new Visit
        {
            PatientId = patientId,
            Date = Date,
            Description = Description
        };

        await _visitRepo.AddAsync(visit);

        visitId = visit.Id;

        foreach (var photo in Photos)
        {
            photo.VisitId = visitId;
            await _photoRepo.AddAsync(photo);
        }

        // ✅ тепер доступ є
        _navigationResult.SetResult(visit);

        await _navigationService.GoBack();
    }
}