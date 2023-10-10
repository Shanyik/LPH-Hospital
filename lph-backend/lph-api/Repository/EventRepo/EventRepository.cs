using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.EventRepo;

public class EventRepository : IEventRepository
{
    public IEnumerable<Event> GetAll()
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Events.ToList();
    }
    
    public Event? GetById(int id)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Events.FirstOrDefault(c => c.Id == id);
    }

    public void Add(Event eventName)
    {
        using var dbContext = new HospitalApiContext();
        dbContext.Add(eventName);
        dbContext.SaveChanges();
    }

    public void Delete(Event eventName)
    {
        using var dbContext = new HospitalApiContext();
        dbContext.Remove(eventName);
        dbContext.SaveChanges();
    }

    public void Update(Event eventName)
    {  
        using var dbContext = new HospitalApiContext();
        dbContext.Update(eventName);
        dbContext.SaveChanges();
    }
}