using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalApp.Core.Models;
using DentalApp.ViewModels;

namespace DentalApp.Pages;

[QueryProperty(nameof(Photo), "photo")]
public partial class PhotoPage : ContentPage
{
    public VisitPhoto Photo
    {
        set => BindingContext = new PhotoViewModel
        {
            Photo = value
        };
    }

    public PhotoPage()
    {
        InitializeComponent();
    }
}