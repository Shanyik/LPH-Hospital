using lph_api.Model;
using lph_api.Repository.PatientRepo;
using Microsoft.AspNetCore.Mvc;

namespace lph_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{

    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try
        {
            var patients = _patientRepository.GetAll();

            if (!patients.Any())
            {
                return NotFound("No patients in database");
            }

            return Ok(patients);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error getting patient data");
            return BadRequest("Error getting patient data");
        }
    }

    [HttpGet("GetByUsername:{username}")]
    public IActionResult GetByUsername(string username)
    {
        try
        {
            var patient = _patientRepository.GetByUsername(username);
            
            if (patient == null)
            {
                return NotFound("No patient with this username in database");
            }

            return Ok(patient);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error getting patient data");
            return BadRequest("Error getting patient data");
        }
    }

    [HttpPost("Add")]
    public IActionResult AddPatient(Patient patient)
    {
        if (patient == null)
        {
            return BadRequest("Missing or not accaptable data.");
        }
        
        try
        {
            _patientRepository.Add(patient);
            return Ok($"patient added with {patient.Username}");
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
            var patient = _patientRepository.GetByUsername(username);
            
            if (patient == null)
            {
                return NotFound("No patient with this username in database");
            }
            _patientRepository.Delete(patient);
            return Ok($"Successfully deleted '{username}' user from database");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error adding patient data");
            return BadRequest("Error adding patient data");
        }
    }
    
}