using lph_api.Model;
using lph_api.Repository.EventRepo;
using Microsoft.AspNetCore.Mvc;

namespace lph_api.Controllers;
[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        try
        {
            var events = _eventRepository.GetAll();

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
    public IActionResult AddEvent(Event eventName)
    {
        if (eventName == null)
        {
            return BadRequest();
        }
        
        try
        {
            _eventRepository.Add(eventName);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    [HttpDelete("Delete:{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var eventName = _eventRepository.GetById(id);
            
            if (eventName == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventName);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}