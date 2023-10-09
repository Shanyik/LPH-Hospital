﻿using lph_api.Model;
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
        //System.Threading.Thread.Sleep(1000);
        try
        {
            var patients = _patientRepository.GetAll().ToList();
            if (patients.Count == 0)
            {
                return NotFound("No patients");
            }

            return Ok(patients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Server error");
        }
        
    }

    [HttpGet("GetPatientByUsername:{username}")]
    public IActionResult GetPatient(string username)
    {
        try
        {
            var res = _patientRepository.GetPatientByName(username);

            if (res == null)
            {
                return NotFound($"Patient not found with {username}");
            }

            return Ok(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Server error");
        }
    }

    [HttpPost("AddPatient")]
    public IActionResult AddPatient(Patient patient) //[FromBody] 
    {
        if (patient == null)
        {
            return BadRequest("Hibás vagy hiányzó adatok a kérésben.");
        }
        try
        {
            _patientRepository.AddPatient(patient);
            return Ok($"patient added with {patient.Username}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Server error");
        }
    }

    [HttpDelete("DeletByUsername:{username}")]
    public IActionResult DeletPatientByUsername(string username)
    {
        try
        {
            Console.WriteLine(username);
            var deletPatient = _patientRepository.GetPatientByName(username);
            
            if (deletPatient == null)
            {
                return NotFound($"Patient not found with {username}");
            }
            _patientRepository.DeletPatient(deletPatient);
            return Ok("asd");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    [HttpPatch("Update")]
    public IActionResult UpdatePatient(Patient patient)
    {
        try
        {
            var res = _patientRepository.GetPatientByName(patient.Username);
            if (res != null)
            {
                return Ok($"");
            }

            return NotFound("");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}