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
                return NotFound("No doctors in database");
            }

            return Ok(doctors);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error getting doctor data");
            return BadRequest("Error getting doctor data");
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
                return NotFound("No doctor with this username in database");
            }

            return Ok(doctor);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error getting doctor data");
            return BadRequest("Error getting doctor data");
        }
    }

    [HttpPost("Add")]
    public IActionResult AddDoctor(Doctor doctor)
    {
        if (doctor == null)
        {
            return BadRequest("Missing or not acceptable data.");
        }
        
        try
        {
            _doctorRepository.Add(doctor);
            return Ok($"doctor added with {doctor.Username}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Server error");
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
                return NotFound("No doctor with this username in database");
            }
            _doctorRepository.Delete(doctor);
            return Ok($"Successfully deleted '{username}' user from database");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error adding doctor data");
            return BadRequest("Error adding doctor data");
        }
    }
}