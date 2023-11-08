using lphh_api.Model;

namespace lphh_api.Repository.EventRepo;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAll();
    Task<Event?> GetById(int id);
    Task Add(Event eventName);
    Task Delete(Event eventName);
    Task Update(Event eventName);
}