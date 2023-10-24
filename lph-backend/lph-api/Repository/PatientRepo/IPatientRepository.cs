using lph_api.Model;

namespace lph_api.Repository.PatientRepo;

public interface IPatientRepository
{
    IEnumerable<Patient> GetAll();
    Patient? GetByUsername(string username);
    public Patient? GetById(int id);
    void Add(Patient patient);
    void Delete(Patient patient);
    void Update(Patient patient);

    Patient? GetByMedicalNumber(string number);
    
    Patient? GetByIdentityId(string id);
}