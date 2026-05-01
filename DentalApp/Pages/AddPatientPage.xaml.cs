using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalApp.ViewModels;

namespace DentalApp.Pages;

public partial class AddPatientPage : ContentPage
{
    public AddPatientPage(AddPatientViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}