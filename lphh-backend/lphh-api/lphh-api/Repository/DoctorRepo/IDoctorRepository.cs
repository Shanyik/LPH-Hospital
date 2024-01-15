using System.Collections.Generic;
using System.Threading.Tasks;
using lphh_api.Model;

namespace lphh_api.Repository.DoctorRepo;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAll();
    Task<Doctor?> GetByUsername(string username);
    Task<Doctor?> GetById(int id);
    Task Add(Doctor doctor);
    Task Delete(Doctor doctor);
    Task Update(Doctor doctor);

    Task<Doctor?> GetByIdentityId(string id);
}