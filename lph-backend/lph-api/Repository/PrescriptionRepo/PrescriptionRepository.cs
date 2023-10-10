using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.PrescriptionRepo;

public class PrescriptionRepository : IPrescriptionRepository
{
    public IEnumerable<Prescription> GetByPatientId(int id)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Prescriptions.Where(c => c.PatientId == id).ToList();
    }
    
    public IEnumerable<Prescription> GetByDoctorId(int id)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Prescriptions.Where(c => c.DoctorId == id).ToList();
    }
    
    public void Add(Prescription prescription)
    {
        using var dbContext = new HospitalApiContext();
        dbContext.Add(prescription);
        dbContext.SaveChanges();
    }
}