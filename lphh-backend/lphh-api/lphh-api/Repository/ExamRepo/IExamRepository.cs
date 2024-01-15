using System.Collections.Generic;
using System.Threading.Tasks;
using lphh_api.Model;

namespace lphh_api.Repository.ExamRepo;

public interface IExamRepository
{
    Task<IEnumerable<Exam>> GetByPatientId(int id);
    Task<IEnumerable<Exam>> GetByDoctorId(int id);
    Task Add(Exam exam);
}