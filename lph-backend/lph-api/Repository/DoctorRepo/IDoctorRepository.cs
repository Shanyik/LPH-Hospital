using lph_api.Model;

namespace lph_api.Repository.DoctorRepo;

public interface IDoctorRepository
{
    IEnumerable<Doctor> GetAll();

    Doctor? GetDoctorByName(string name);

    void AddDoctor(Doctor city);
    
    void DeletDoctor(Doctor city);
    
    void UpdateDoctor(Doctor city);
}