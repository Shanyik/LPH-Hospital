using lph_api.Context;
using lph_api.Model;

namespace lph_api.Repository.ProductRepo;

public class ProductRepository : IProductRepository
{
    public IEnumerable<Product> GetAll()
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Products.ToList();
    }

    public Product? GetByName(string name)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Products.FirstOrDefault(c => c.Name == name);
    }
    
    public Product? GetById(int id)
    {
        using var dbContext = new HospitalApiContext();
        return dbContext.Products.FirstOrDefault(c => c.Id == id);
    }
}