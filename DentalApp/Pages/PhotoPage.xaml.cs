using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalApp.Core.Models;
using DentalApp.ViewModels;

namespace DentalApp.Pages;

public partial class PhotoPage : ContentPage, IQueryAttributable
{
    private readonly PhotoViewModel _vm;

    public PhotoPage(PhotoViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
        _vm = vm;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("photo", out var value) &&
            value is VisitPhoto photo)
        {
            _vm.Init(photo);
        }
    }
}