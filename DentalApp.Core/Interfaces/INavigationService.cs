using DentalApp.Core.Models;

namespace DentalApp.Core.Interfaces;

public interface INavigationService
{
    // Patients
    Task GoToPatientDetails(int patientId);
    Task GoToAddPatient();

    // Visits
    Task<Visit?> GoToAddVisit(int patientId);
    Task GoToVisitDetails(int visitId);

    // Photos
    Task GoToPhoto(VisitPhoto photo);

    // Base
    Task GoBack();
}