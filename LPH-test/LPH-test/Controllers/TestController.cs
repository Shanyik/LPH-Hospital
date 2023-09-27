using Microsoft.AspNetCore.Mvc;

namespace LPH_test.Controllers;


[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult Get()
    {
        
        
        return Ok("{yes}");
    }
}