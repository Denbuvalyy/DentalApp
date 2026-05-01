using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;
using DentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Infrastructure.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly AppDbContext _context;

    public VisitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Visit>> GetByPatientIdAsync(int patientId)
        => await _context.Visits
            .Where(v => v.PatientId == patientId)
            .OrderByDescending(v => v.Date)
            .ToListAsync();

    public async Task AddAsync(Visit visit)
    {
        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();
    }
}