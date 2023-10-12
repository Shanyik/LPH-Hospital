using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.PatientRepo;

public class PatientRepository : IPatientRepository
{
    public IEnumerable<Patient> GetAll()
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Patients.ToList();
    }

    public Patient? GetByUsername(string username)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Patients.FirstOrDefault(c => c.Username == username);
    }
    
    public Patient? GetById(int id)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Patients.FirstOrDefault(c => c.Id == id);
    }

    public void Add(Patient patient)
    {
        using var dbContext = new HospitalApiContext();
        dbContext.Add(patient);
        dbContext.SaveChanges();
    }

    public void Delete(Patient patient)
    {
        using var dbContext = new HospitalApiContext();
        dbContext.Remove(patient);
        dbContext.SaveChanges();
    }

    public void Update(Patient patient)
    {  
        using var dbContext = new HospitalApiContext();
        dbContext.Update(patient);
        dbContext.SaveChanges();
    }
}