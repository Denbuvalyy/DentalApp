using DentalApp.Core.Models;
using DentalApp.ViewModels;

namespace DentalApp.Pages;

public partial class GalleryPage : ContentPage, IQueryAttributable
{
    private readonly GalleryViewModel _vm;

    public GalleryPage(GalleryViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
        _vm = vm;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var photos = query["photos"] as List<VisitPhoto>;
        var index = (int)query["index"];

        _vm.Init(photos!, index);
    }
}