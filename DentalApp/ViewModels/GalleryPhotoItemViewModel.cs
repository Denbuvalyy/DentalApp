using CommunityToolkit.Mvvm.ComponentModel;
using DentalApp.Core.Models;

namespace DentalApp.ViewModels;

public partial class GalleryPhotoItemViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isZoomed;
    public VisitPhoto Model { get; }

    public GalleryPhotoItemViewModel(VisitPhoto model)
    {
        Model = model;
    }

    public ImageSource Image =>
        ImageSource.FromFile(Model.FilePath);
}