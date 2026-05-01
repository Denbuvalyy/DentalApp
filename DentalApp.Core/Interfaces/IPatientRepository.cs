using DentalApp.Core.Models;

namespace DentalApp.Core.Interfaces;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllAsync();
    Task<Patient?> GetAsync(int id);
    Task AddAsync(Patient patient);
}