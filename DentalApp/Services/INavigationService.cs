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
    Task GoToGallery(
        List<VisitPhoto> photos,
        int startIndex);
    //Task GoToPhoto(VisitPhoto photo);

    // Base
    Task GoBack();
}