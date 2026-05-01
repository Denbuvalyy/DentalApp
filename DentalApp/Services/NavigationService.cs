using DentalApp.Constants;
using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;

namespace DentalApp.Services;

public class NavigationService : INavigationService, INavigationResult
{
    private readonly Stack<TaskCompletionSource<object?>> _resultStack = new();

    // =========================
    // 🔹 BASE
    // =========================

    public Task GoBack()
    {
        return Shell.Current.GoToAsync("..");
    }

    public void SetResult(object? result)
    {
        if (_resultStack.Count == 0)
            return;

        var tcs = _resultStack.Pop();
        tcs.TrySetResult(result);
    }

    private async Task<T?> NavigateForResult<T>(string route, IDictionary<string, object>? parameters = null)
    {
        var tcs = new TaskCompletionSource<object?>();
        _resultStack.Push(tcs);

        if (parameters != null)
            await Shell.Current.GoToAsync(route, parameters);
        else
            await Shell.Current.GoToAsync(route);

        var result = await tcs.Task;

        return (T?)result;
    }

    private Task Navigate(string route, IDictionary<string, object>? parameters = null)
    {
        if (parameters != null)
            return Shell.Current.GoToAsync(route, parameters);

        return Shell.Current.GoToAsync(route);
    }

    // =========================
    // 🔹 PATIENTS
    // =========================

    public Task GoToPatientDetails(int patientId)
    {
        return Navigate(Routes.PatientDetails, new Dictionary<string, object>
        {
            ["patientId"] = patientId
        });
    }

    public Task GoToAddPatient()
    {
        return Navigate(Routes.AddPatient);
    }

    // =========================
    // 🔹 VISITS
    // =========================

    public Task<Visit?> GoToAddVisit(int patientId)
    {
        return NavigateForResult<Visit>(Routes.AddVisit, new Dictionary<string, object>
        {
            ["patientId"] = patientId
        });
    }

    public Task GoToVisitDetails(int visitId)
    {
        return Navigate(Routes.VisitDetails, new Dictionary<string, object>
        {
            ["visitId"] = visitId
        });
    }

    // =========================
    // 🔹 PHOTOS
    // =========================

    public Task GoToPhoto(VisitPhoto photo)
    {
        return Navigate(Routes.Photo, new Dictionary<string, object>
        {
            ["photo"] = photo
        });
    }
}