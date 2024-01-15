using System.Collections.Generic;
using System.Formats.Asn1;
using System.Threading.Tasks;
using lphh_api.Context;
using lphh_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lphh_api.Repository.PatientRepo;

public class PatientRepository : IPatientRepository
{
    private HospitalApiContext _context;

    public PatientRepository(HospitalApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAll()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient?> GetByUsername(string username)
    {
        return await _context.Patients.FirstOrDefaultAsync(c => c.Username == username);
    }

    public async Task<Patient?> GetById(int id)
    {
        return await _context.Patients.FirstOrDefaultAsync(c => c.Id == id);
    }


    public async Task Add(Patient patient)
    {
        await _context.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Patient patient)
    {
        _context.Remove(patient);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Patient patient)
    {
        _context.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<Patient?> GetByMedicalNumber(string number)
    {
        return await _context.Patients.FirstOrDefaultAsync(c => c.MedicalNumber == number);
    }

    public async Task<Patient?> GetByIdentityId(string id)
    {
        return await _context.Patients.FirstOrDefaultAsync(c => c.IdentityId == id);
    }

}