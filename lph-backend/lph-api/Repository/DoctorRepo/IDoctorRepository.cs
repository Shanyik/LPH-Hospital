using lph_api.Model;

namespace lph_api.Repository.DoctorRepo;

public interface IDoctorRepository
{
    IEnumerable<Doctor> GetAll();
    Doctor? GetByUsername(string username);
    public Doctor? GetById(int id);
    void Add(Doctor doctor);
    void Delete(Doctor doctor);
    void Update(Doctor doctor);

    Doctor? GetByIdentityId(string id);
}