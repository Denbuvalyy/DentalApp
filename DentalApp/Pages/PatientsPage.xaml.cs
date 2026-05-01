using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalApp.ViewModels;

namespace DentalApp.Pages;

public partial class PatientsPage : ContentPage
{
    public PatientsPage(PatientsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PatientsViewModel vm)
            vm.LoadCommand.Execute(null);
    }
}