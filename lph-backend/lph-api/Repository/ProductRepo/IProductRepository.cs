using lph_api.Model;

namespace lph_api.Repository.ProductRepo;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetByName(string name);
    Task<Product?> GetById(int id);
}