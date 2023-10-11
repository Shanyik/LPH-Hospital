using lph_api.Model;
using lph_api.Repository.DoctorRepo;
using Microsoft.AspNetCore.Mvc;

namespace lph_api.Controllers;

[ApiController]
[Route("[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorController(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }
    
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try
        {
            var doctors = _doctorRepository.GetAll();

            if (!doctors.Any())
            {
                return NotFound();
            }

            return Ok(doctors);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpGet("GetByUsername:{username}")]
    public IActionResult GetByUsername(string username)
    {
        try
        {
            var doctor = _doctorRepository.GetByUsername(username);
            
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("Add")]
    public IActionResult AddDoctor(Doctor doctor)
    {
        if (doctor == null)
        {
            return BadRequest();
        }
        
        try
        {
            _doctorRepository.Add(doctor);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("Delete:{username}")]
    public IActionResult Delete(string username)
    {
        try
        {
            var doctor = _doctorRepository.GetByUsername(username);
            
            if (doctor == null)
            {
                return NotFound();
            }
            _doctorRepository.Delete(doctor);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}