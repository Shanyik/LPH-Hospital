using System;
using System.Linq;
using System.Threading.Tasks;
using lphh_api.Context;
using lphh_api.Repository.PrescriptionRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lphh_api.Model;
using Microsoft.AspNetCore.Authorization;

namespace lphh_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    
    

    public PrescriptionController(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        
    }
    
    [HttpGet("GetByPatientId:{id}")]
    [Authorize(Roles = "Doctor, Patient, Admin")]
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
    [Authorize(Roles = "Doctor, Patient, Admin")]
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
    [Authorize(Roles = "Doctor, Admin")]
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