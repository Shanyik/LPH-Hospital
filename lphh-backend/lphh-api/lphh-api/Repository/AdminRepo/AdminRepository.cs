﻿using System.Threading.Tasks;
using lphh_api.Context;
using lphh_api.Model;
using Microsoft.EntityFrameworkCore;

namespace lphh_api.Repository.AdminRepo;

public class AdminRepository : IAdminRepository
{
    private readonly HospitalApiContext _context;
    
    public AdminRepository(HospitalApiContext context)
    {
        _context = context;
    }
    
    public async Task<Admin?> GetById(int id)
    {
        return await _context.Admins.FirstOrDefaultAsync(c => c.Id == (uint)id);
    }
    
    public async Task<Admin?> GetByIdentityId(string id)
    {
        return await _context.Admins.FirstOrDefaultAsync(c => c.IdentityId == id);
    }
    
    public async Task Add(Admin admin)
    {
        await _context.AddAsync(admin);
        await _context.SaveChangesAsync();
    }
}
