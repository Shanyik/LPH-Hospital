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
    public IActionResult GetByPatientId(int id)
    {
        try
        {
            var exams = _examRepository.GetByPatientId(id);

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
    public IActionResult GetByDoctorId(int id)
    {
        try
        {
            var exams = _examRepository.GetByDoctorId(id);
            
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
    
    //ADD
}