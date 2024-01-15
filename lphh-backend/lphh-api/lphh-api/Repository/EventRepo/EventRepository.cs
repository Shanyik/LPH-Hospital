using System.Collections.Generic;
using System.Threading.Tasks;
using lphh_api.Context;
using lphh_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lphh_api.Repository.EventRepo;

public class EventRepository : IEventRepository
{
    private readonly HospitalApiContext _context;

    public EventRepository(HospitalApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Event>> GetAll()
    {
        return await _context.Events.ToListAsync();
    }
    
    public async Task<Event?> GetById(int id)
    {
        return await _context.Events.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Add(Event eventName)
    {
        await _context.AddAsync(eventName);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Event eventName)
    {
        _context.Remove(eventName);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Event eventName)
    {
        _context.Update(eventName);
        await _context.SaveChangesAsync();
    }
}