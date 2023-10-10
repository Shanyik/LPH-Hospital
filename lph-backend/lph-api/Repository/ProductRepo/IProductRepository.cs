using lph_api.Model;

namespace lph_api.Repository.ProductRepo;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetByName(string name);
}