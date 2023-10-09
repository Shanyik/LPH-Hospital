using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.DoctorRepo;

public class DoctorRepository : IDoctorRepository
{
    public IEnumerable<Doctor> GetAll()
    {
        using var dbContext = new HospitalApi();
        return dbContext.Doctors.ToList();
    }

    public Doctor? GetDoctorByName(string username)
    {
        using var dbContext = new HospitalApi();
        return dbContext.Doctors.FirstOrDefault(c => c.Username == username);
    }

    public void AddDoctor(Doctor doctor)
    {
        using var dbContext = new HospitalApi();
        dbContext.Add(doctor);
        dbContext.SaveChanges();
    }

    public void DeletDoctor(Doctor doctor)
    {
        using var dbContext = new HospitalApi();
        dbContext.Remove(doctor);
        dbContext.SaveChanges();
    }

    public void UpdateDoctor(Doctor doctor)
    {
        using var dbContext = new HospitalApi();
        dbContext.Update(doctor);
        dbContext.SaveChanges();
    }
}