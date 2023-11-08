using lphh_api.Context;
using lphh_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lphh_api.Repository.DoctorRepo;

public class DoctorRepository : IDoctorRepository
{

    private readonly HospitalApiContext _context;

    public DoctorRepository(HospitalApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAll()
    {
        return await _context.Doctors.ToListAsync();
    }

    public async Task<Doctor?> GetByUsername(string username)
    {
        return await _context.Doctors.FirstOrDefaultAsync(c => c.Username == username);
    }
    
    public async Task<Doctor?> GetById(int id)
    {
        return await _context.Doctors.FirstOrDefaultAsync(c => c.Id == (uint)id);
    }

    public async Task Add(Doctor doctor)
    {
        await _context.AddAsync(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Doctor doctor)
    {
        _context.Remove(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Doctor doctor)
    { 
        _context.Update(doctor);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Doctor?> GetByIdentityId(string id)
    {
        return await _context.Doctors.FirstOrDefaultAsync(c => c.IdentityId == id);
    }
    
}