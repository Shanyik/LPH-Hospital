using lph_api.Model;

namespace lph_api.Repository.EventRepo;

public interface IEventRepository
{
    IEnumerable<Event> GetAll();
    public Event? GetById(int id);
    void Add(Event eventName);
    void Delete(Event eventName);
    void Update(Event eventName);
}