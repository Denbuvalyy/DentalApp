using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalApp.ViewModels;

namespace DentalApp.Pages;

public partial class VisitDetailsPage : ContentPage, IQueryAttributable
{
    private readonly VisitDetailsViewModel _vm;

    public VisitDetailsPage(VisitDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("visitId", out var value) &&
            value is int id)// &&
            //int.TryParse(str, out var id))
        {
            _ = _vm.Init(id);
        }
    }
}