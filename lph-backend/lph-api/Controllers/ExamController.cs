using lph_api.Model;
using lph_api.Repository.ExamRepo;
using Microsoft.AspNetCore.Mvc;

namespace lph_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExamController : ControllerBase
{
    private readonly IExamRepository _examRepository;

    public ExamController(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }
    
    [HttpGet("GetByPatientId:{id}")]
    public async Task<IActionResult> GetByPatientId(int id)
    {
        try
        {
            var exams = await _examRepository.GetByPatientId(id);

            if (!exams.Any())
            {
                return NotFound();
            }

            return Ok(exams);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("GetByDoctorId:{id}")]
    public async Task<IActionResult> GetByDoctorId(int id)
    {
        try
        {
            var exams = await _examRepository.GetByDoctorId(id);
            
            if (!exams.Any())
            {
                return NotFound();
            }

            return Ok(exams);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpPost("Add")]
    public async Task<IActionResult> AddExam(Exam exam)
    {
        try
        {
            await _examRepository.Add(exam);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
}