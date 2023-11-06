using lph_api.Model;

namespace lph_api.Repository.PrescriptionRepo;

public interface IPrescriptionRepository
{
    Task<IEnumerable<Prescription>> GetByPatientId(int id);
    Task<IEnumerable<Prescription>> GetByDoctorId(int id);
    Task Add(Prescription prescription);
}