using System.Text.Json.Nodes;
using lphh_api.Model;
using lphh_api.Repository.PatientRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{

    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    
    [HttpGet("GetAll")]
    [Authorize(Roles = "Doctor, Patient, Admin")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var patients = await _patientRepository.GetAll();

            if (!patients.Any())
            {

                return NotFound();
            }

            return Ok(patients);
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
            var patient = await _patientRepository.GetByUsername(username);
            
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
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
            var patient = await _patientRepository.GetById(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    

    [HttpPost("Add")]
    [Authorize(Roles = "Doctor, Admin")]
    public async Task<IActionResult> AddPatient(Patient patient)
    {
        if (patient == null)
        {
            return BadRequest();
        }
        
        try
        {
            await _patientRepository.Add(patient);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete("Delete:{username}")]
    [Authorize(Roles = "Doctor, Admin")]
    public async Task<IActionResult> Delete(string username)
    {
        try
        {
            var patient = await _patientRepository.GetByUsername(username);
            
            if (patient == null)
            {
                return NotFound();
            }
            await _patientRepository.Delete(patient);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("GetByMedicalNumber:{medicalNumber}")]
    [Authorize(Roles = "Doctor, Patient, Admin")]
    public async Task<IActionResult> GetByMedicalNumber(string medicalNumber)
    {
        try
        {
            var patient = await _patientRepository.GetByMedicalNumber(medicalNumber);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("GetByIdentityId:{id}")]
    [Authorize(Roles = "Doctor, Patient, Admin")]
    public async Task<IActionResult> GetByIdentityId(string id)
    {
        try
        {
            var patient = await _patientRepository.GetByIdentityId(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}