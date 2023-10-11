﻿using lph_api.Repository.PrescriptionRepo;
using Microsoft.AspNetCore.Mvc;

namespace lph_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionController(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }
    
    [HttpGet("GetByPatientId:{id}")]
    public IActionResult GetByPatientId(int id)
    {
        try
        {
            var prescriptions = _prescriptionRepository.GetByPatientId(id);
            
            if (!prescriptions.Any())
            {
                return NotFound();
            }

            return Ok(prescriptions);
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
            var prescriptions = _prescriptionRepository.GetByDoctorId(id);
            
            if (!prescriptions.Any())
            {
                return NotFound();
            }

            return Ok(prescriptions);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    //ADD
}