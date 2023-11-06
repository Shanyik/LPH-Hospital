using lph_api.Context;
using lph_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lph_api.Repository.PrescriptionRepo;

public class PrescriptionRepository : IPrescriptionRepository
{

    private readonly HospitalApiContext _context;

    public PrescriptionRepository(HospitalApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prescription>> GetByPatientId(int id)
    {
        return await _context.Prescriptions.Where(c => c.PatientId == id).ToListAsync();
    }
    
    public async Task<IEnumerable<Prescription>> GetByDoctorId(int id)
    {
        return await _context.Prescriptions.Where(c => c.DoctorId == id).ToListAsync();
    }
    
    public async Task Add(Prescription prescription)
    {
       await _context.AddAsync(prescription);
       await _context.SaveChangesAsync();
    }
}