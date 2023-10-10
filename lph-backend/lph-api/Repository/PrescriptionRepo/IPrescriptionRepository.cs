using lph_api.Model;

namespace lph_api.Repository.PrescriptionRepo;

public interface IPrescriptionRepository
{
    IEnumerable<Prescription> GetByPatientId(int id);
    IEnumerable<Prescription> GetByDoctorId(int id);
    void Add(Prescription prescription);
}