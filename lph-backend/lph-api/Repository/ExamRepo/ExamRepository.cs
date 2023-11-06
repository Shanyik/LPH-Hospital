using lph_api.Context;
using lph_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lph_api.Repository.ExamRepo;

public class ExamRepository : IExamRepository
{
    private readonly HospitalApiContext _context;

    public ExamRepository(HospitalApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Exam>> GetByPatientId(int id)
    {
        
        return await _context.Exams.Where(c => c.PatientId == id).ToListAsync();
    }
    
    public async Task<IEnumerable<Exam>> GetByDoctorId(int id)
    {
        
        return await _context.Exams.Where(c => c.DoctorId == id).ToListAsync();
    }
    
    public async Task Add(Exam exam)
    {
        
        await _context.AddAsync(exam);
        await _context.SaveChangesAsync();
    }
}