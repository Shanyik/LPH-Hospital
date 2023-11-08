using lphh_api.Model;
using lphh_api.Repository.EventRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var events = await _eventRepository.GetAll();

            if (!events.Any())
            {
                return NotFound();
            }

            return Ok(events);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("Add")]
    [Authorize(Roles = "Doctor, Admin")]
    public async Task<IActionResult> AddEvent(Event eventName)
    {
        if (eventName == null)
        {
            return BadRequest();
        }
        
        try
        {
            await _eventRepository.Add(eventName);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete("Delete:{id}")]
    [Authorize(Roles = "Doctor, Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var eventName = await _eventRepository.GetById(id);
            
            if (eventName == null)
            {
                return NotFound();
            }
            await _eventRepository.Delete(eventName);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}