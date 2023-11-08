using lphh_api.Model;
using lphh_api.Repository.DoctorRepo;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var doctors = await _doctorRepository.GetAll();

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
    public async Task<IActionResult> GetByUsername(string username)
    {
        try
        {
            var doctor = await _doctorRepository.GetByUsername(username);
            
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
    
    [HttpGet("GetById:{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var doctor = await _doctorRepository.GetById(id);
            
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
    public async Task<IActionResult> AddDoctor(Doctor doctor)
    {
        if (doctor == null)
        {
            return BadRequest();
        }
        
        try
        {
            await _doctorRepository.Add(doctor);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("Delete:{username}")]
    public async Task<IActionResult> Delete(string username)
    {
        try
        {
            var doctor = await _doctorRepository.GetByUsername(username);
            
            if (doctor == null)
            {
                return NotFound();
            }
            await _doctorRepository.Delete(doctor);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("GetByIdentityId:{id}")]
    public async Task<IActionResult> GetByIdentityId(string id)
    {
        try
        {
            var doctor = await _doctorRepository.GetByIdentityId(id);

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
}