using DentalApp.Core.Models;

namespace DentalApp.Core.Interfaces;

public interface IVisitPhotoRepository
{
    Task<List<VisitPhoto>> GetByVisitIdAsync(int visitId);
    Task AddAsync(VisitPhoto photo);
    Task DeleteAsync(VisitPhoto photo);
}