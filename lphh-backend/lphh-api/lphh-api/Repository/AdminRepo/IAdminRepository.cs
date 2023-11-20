using lphh_api.Model;

namespace lphh_api.Repository.AdminRepo;

public interface IAdminRepository
{
    Task<Admin?> GetById(int id);

    Task Add(Admin admin);

    Task<Admin?> GetByIdentityId(string id);
}