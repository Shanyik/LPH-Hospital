using lphh_api.Model;

namespace lphh_api.Repository.AdminRepo;

public interface IAdminRepository
{
    Task<Doctor?> GetByIdentityId(string id);

    Task Add(Admin admin);
}