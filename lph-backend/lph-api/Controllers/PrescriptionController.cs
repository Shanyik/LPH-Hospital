﻿using lph_api.Context;
using lph_api.Repository.PrescriptionRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lph_api.Model;

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
    public async Task<IActionResult> GetByPatientId(int id)
    {
        try
        {
            var prescriptions = await _prescriptionRepository.GetByPatientId(id);

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
    public async Task<IActionResult> GetByDoctorId(int id)
    {
        try
        {
            var prescriptions = await _prescriptionRepository.GetByDoctorId(id);
            
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
    
    [HttpPost("Add")]
    public async Task<IActionResult> Add(Prescription prescription)
    {
        if (prescription == null)
        {
            return BadRequest();
        }
        
        try
        {
            await _prescriptionRepository.Add(prescription);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}