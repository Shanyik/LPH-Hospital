using System;
using System.Threading.Tasks;
using lphh_api.Model;
using lphh_api.Repository.AdminRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository _adminRepository;

    public AdminController(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    
    [HttpPost("Add")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddDoctor(Admin admin)
    {
        if (admin == null)
        {
            return BadRequest();
        }
        
        try
        {
            await _adminRepository.Add(admin);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("GetById:{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var admin = await _adminRepository.GetById(id);
            
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpGet("GetByIdentityId:{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByIdentityId(string id)
    {
        try
        {
            var admin = await _adminRepository.GetByIdentityId(id);

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
}