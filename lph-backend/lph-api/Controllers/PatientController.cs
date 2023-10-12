﻿using System.Text.Json.Nodes;
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
    public IActionResult GetByUsername(string username)
    {
        try
        {
            var patient = _patientRepository.GetByUsername(username);
            
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
    public IActionResult GetById(int id)
    {
        try
        {
            var patient = _patientRepository.GetById(id);

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
    public IActionResult AddPatient(Patient patient)
    {
        if (patient == null)
        {
            return BadRequest();
        }
        
        try
        {
            _patientRepository.Add(patient);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
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
                return NotFound();
            }
            _patientRepository.Delete(patient);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("GetByMedicalNumber:{medicalNumber}")]
    public IActionResult GetByMedicalNumber(string medicalNumber)
    {
        try
        {
            var patient = _patientRepository.GetByMedicalNumber(medicalNumber);

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