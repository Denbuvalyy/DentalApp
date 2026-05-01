using DentalApp.Core.Interfaces;
using DentalApp.Core.Models;
using DentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetAllAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetAsync(int id)
    {
        return await _context.Patients
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
    }
}