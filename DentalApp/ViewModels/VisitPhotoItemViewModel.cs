using CommunityToolkit.Mvvm.ComponentModel;
using DentalApp.Core.Models;

namespace DentalApp.ViewModels;

public partial class VisitPhotoItemViewModel : ObservableObject
{
    public VisitPhoto Model { get; }

    public VisitPhotoItemViewModel(VisitPhoto model)
    {
        Model = model;
    }

    public int Id => Model.Id;

    public string FilePath => Model.FilePath;

    public ImageSource Image =>
        ImageSource.FromFile(Model.FilePath);
}