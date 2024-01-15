using System;
using System.Linq;
using System.Threading.Tasks;
using lphh_api.Model;
using lphh_api.Repository.ExamRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController : ControllerBase
{
    private readonly IExamRepository _examRepository;

    public ExamController(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }
    
    [HttpGet("GetByPatientId:{id}")]
    [Authorize(Roles = "Doctor, Patient, Admin")]
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
    [Authorize(Roles = "Doctor, Patient, Admin")]
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
    [Authorize(Roles = "Doctor, Admin")]
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