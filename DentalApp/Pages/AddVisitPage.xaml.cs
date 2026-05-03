using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalApp.ViewModels;

namespace DentalApp.Pages;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class AddVisitPage : ContentPage
{
    private readonly AddVisitViewModel _vm;

    public string PatientId
    {
        set
        {
            if (int.TryParse(value, out var id))
                _vm.Init(id);
        }
    }

    public AddVisitPage(AddVisitViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}