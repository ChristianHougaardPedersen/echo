using Application.DAOInterfaces;
using Application.ProviderInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDAO : IUserDAO, IUserProvider 
{
    private readonly EchoContext context;

    public UserEfcDAO(EchoContext context)
    {
        this.context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO parameters)
    {
        IQueryable<User> users = context.Users.AsQueryable();
        if (parameters.UsernameContains != null)
        {
            users = context.Users.Where(user =>
                user.Username.ToLower().Contains(parameters.UsernameContains.ToLower()));
        }

        IEnumerable<User> result = await users.ToListAsync();

        return result;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        User? existing =
            await context.Users.FirstOrDefaultAsync(user =>
     
                user.Username.ToLower().Equals(username.ToLower()));
        return existing;
    }

}