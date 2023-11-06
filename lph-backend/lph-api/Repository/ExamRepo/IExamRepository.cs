using lph_api.Model;

namespace lph_api.Repository.ExamRepo;

public interface IExamRepository
{
    Task<IEnumerable<Exam>> GetByPatientId(int id);
    Task<IEnumerable<Exam>> GetByDoctorId(int id);
    Task Add(Exam exam);
}