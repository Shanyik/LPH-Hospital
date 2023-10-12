using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.PrescriptionRepo;

public class PrescriptionRepository : IPrescriptionRepository
{
    public IEnumerable<Prescription> GetByPatientId(int id)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Prescriptions.Where(c => c.PatientId == id).ToList();
        
        /*
         var person = (
            from p in dbContext.Prescriptions
            join d in dbContext.Doctors 
                on p.DoctorId equals d.Id    
            where p.Id == id    
            select p);
         */
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