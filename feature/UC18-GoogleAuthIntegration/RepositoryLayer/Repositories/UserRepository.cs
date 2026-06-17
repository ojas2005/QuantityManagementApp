using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly QuantityDbContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(QuantityDbContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Set<User>().FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetByGoogleIdAsync(string googleId)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.GoogleId == googleId);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Set<User>().Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Set<User>().Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user == null) return false;
        
        _context.Set<User>().Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(string email, string username)
    {
        return await _context.Set<User>().AnyAsync(u => u.Email == email || u.Username == username);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Set<User>().ToListAsync();
    }
}
