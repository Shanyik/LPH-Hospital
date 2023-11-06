using lph_api.Context;
using lph_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lph_api.Repository.ProductRepo;

public class ProductRepository : IProductRepository
{
    private readonly HospitalApiContext _context;

    public ProductRepository(HospitalApiContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByName(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(c => c.Name == name);
    }
    
    public async Task<Product?> GetById(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
    }
}