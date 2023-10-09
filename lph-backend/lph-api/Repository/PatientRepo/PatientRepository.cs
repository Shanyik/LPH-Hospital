using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.PatientRepo;

public class PatientRepository : IPatientRepository
{
    public IEnumerable<Patient> GetAll()
    {
        using var dbContext = new HospitalApi();
        return dbContext.Patients.ToList();
    }

    public Patient? GetPatientByName(string username)
    {
        using var dbContext = new HospitalApi();
        return dbContext.Patients.FirstOrDefault(c => c.Username == username);
    }

    public void AddPatient(Patient patient)
    {
        using var dbContext = new HospitalApi();
        dbContext.Add(patient);
        dbContext.SaveChanges();
    }

    public void DeletPatient(Patient patient)
    {
        using var dbContext = new HospitalApi();
        dbContext.Remove(patient);
        dbContext.SaveChanges();
    }

    public void UpdatePatient(Patient patient)
    {
        using var dbContext = new HospitalApi();
        dbContext.Update(patient);
        dbContext.SaveChanges();
    }
}