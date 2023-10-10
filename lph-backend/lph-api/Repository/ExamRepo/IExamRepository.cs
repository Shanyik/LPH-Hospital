using lph_api.Model;

namespace lph_api.Repository.ExamRepo;

public interface IExamRepository
{
    IEnumerable<Exam> GetByPatientId(int id);
    IEnumerable<Exam> GetByDoctorId(int id);
    void Add(Exam exam);
}