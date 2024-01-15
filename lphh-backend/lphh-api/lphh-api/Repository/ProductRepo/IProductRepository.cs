using System.Collections.Generic;
using System.Threading.Tasks;
using lphh_api.Model;

namespace lphh_api.Repository.ProductRepo;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetByName(string name);
    Task<Product?> GetById(int id);
}