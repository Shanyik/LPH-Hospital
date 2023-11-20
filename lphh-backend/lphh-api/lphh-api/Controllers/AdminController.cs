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
    
    [HttpGet("GetByIdentityId:{id}")]
    [Authorize(Roles = "Doctor, Admin")]
    public async Task<IActionResult> GetByIdentityId(string id)
    {
        try
        {
            var doctor = await _adminRepository.GetByIdentityId(id);

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