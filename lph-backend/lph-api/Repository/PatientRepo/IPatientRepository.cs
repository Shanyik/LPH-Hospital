using lph_api.Model;

namespace lph_api.Repository.PatientRepo;

public interface IPatientRepository
{
    IEnumerable<Patient> GetAll();

    Patient? GetPatientByName(string name);

    void AddPatient(Patient patient);
    
    void DeletPatient(Patient patient);
    
    void UpdatePatient(Patient patient);
}