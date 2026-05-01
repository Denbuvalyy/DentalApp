using DentalApp.Core.Models;

namespace DentalApp.Core.Interfaces;

public interface IVisitRepository
{
    Task<List<Visit>> GetByPatientIdAsync(int patientId);
    Task AddAsync(Visit visit);
}