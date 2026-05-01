using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;
using DentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Infrastructure.Repositories;

public class VisitPhotoRepository : IVisitPhotoRepository
{
    private readonly AppDbContext _context;

    public VisitPhotoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<VisitPhoto>> GetByVisitIdAsync(int visitId)
    {
        return await _context.VisitPhotos
            .Where(x => x.VisitId == visitId)
            .ToListAsync();
    }

    public async Task AddAsync(VisitPhoto photo)
    {
        _context.VisitPhotos.Add(photo);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(VisitPhoto photo)
    {
        _context.VisitPhotos.Remove(photo);
        await _context.SaveChangesAsync();
    }
}