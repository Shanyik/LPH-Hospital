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
                return NotFound("No events in database");
            }

            return Ok(events);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error getting event data");
            return BadRequest("Error getting event data");
        }
    }

    [HttpPost("Add")]
    public IActionResult AddEvent(Event eventName)
    {
        if (eventName == null)
        {
            return BadRequest("Missing or not acceptable data.");
        }
        
        try
        {
            _eventRepository.Add(eventName);
            return Ok($"doctor added with {eventName.Name}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Server error");
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
                return NotFound("No event with this id in database");
            }
            _eventRepository.Delete(eventName);
            return Ok($"Successfully deleted event from database");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error adding event data");
            return BadRequest("Error adding event data");
        }
    }
}