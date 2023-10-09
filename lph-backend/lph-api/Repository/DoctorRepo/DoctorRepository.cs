﻿using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.DoctorRepo;

public class DoctorRepository : IDoctorRepository
{
    public IEnumerable<Doctor> GetAll()
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Doctors.ToList();
    }

    public Doctor? GetByUsername(string username)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Doctors.FirstOrDefault(c => c.Username == username);
    }

    public void Add(Doctor doctor)
    {
        using var dbContext = new HospitalApiContext();
        dbContext.Add(doctor);
        dbContext.SaveChanges();
    }

    public void Delete(Doctor doctor)
    {
        using var dbContext = new HospitalApiContext();
        dbContext.Remove(doctor);
        dbContext.SaveChanges();
    }

    public void Update(Doctor doctor)
    {  
        using var dbContext = new HospitalApiContext();
        dbContext.Update(doctor);
        dbContext.SaveChanges();
    }
}