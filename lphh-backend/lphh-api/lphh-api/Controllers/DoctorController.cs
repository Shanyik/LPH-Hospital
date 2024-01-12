using lphh_api.Model;
using lphh_api.Repository.DoctorRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorController(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }
    
    [HttpGet("GetAll")]
    [Authorize(Roles = "Doctor, Patient, Admin")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var doctors = await _doctorRepository.GetAll();
            var user = User;
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
    [Authorize(Roles = "Doctor, Patient, Admin")]
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
    [Authorize(Roles = "Doctor, Patient, Admin")]
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
    [Authorize(Roles = "Doctor, Admin")]
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
    [Authorize(Roles = "Doctor, Admin")]
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
    [Authorize(Roles = "Doctor, Admin")]
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