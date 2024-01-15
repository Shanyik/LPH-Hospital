using System.Collections.Generic;
using System.Threading.Tasks;
using lphh_api.Model;

namespace lphh_api.Repository.PatientRepo;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAll();
    Task<Patient?> GetByUsername(string username);
    Task<Patient?> GetById(int id);
    Task Add(Patient patient);
    Task Delete(Patient patient);
    Task Update(Patient patient);

    Task<Patient?> GetByMedicalNumber(string number);
    
    Task<Patient?> GetByIdentityId(string id);
}