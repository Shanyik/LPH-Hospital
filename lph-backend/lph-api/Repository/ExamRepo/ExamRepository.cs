using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.ExamRepo;

public class ExamRepository : IExamRepository
{
    public IEnumerable<Exam> GetByPatientId(int id)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Exams.Where(c => c.PatientId == id).ToList();
    }
    
    public IEnumerable<Exam> GetByDoctorId(int id)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Exams.Where(c => c.DoctorId == id).ToList();
    }
    
    public void Add(Exam exam)
    {
        using var dbContext = new HospitalApiContext();
        dbContext.Add(exam);
        dbContext.SaveChanges();
    }
}