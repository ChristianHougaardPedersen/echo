using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Application.ProviderInterfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDAO dao;
    private readonly IUserProvider provider;
    
    public UserLogic(IUserDAO dao, IUserProvider provider)
    {
        this.dao = dao;
        this.provider = provider;
    }

    public async Task<User> CreateAsync(UserCreationDTO dto)
    {
        User? existing = await provider.GetByUsernameAsync(dto.Username);
        if (existing != null)
        {
            throw new UserValidationException($"Username {dto.Username} is unavailable.");
        }

        ValidateData(dto);
        User toCreate = new User
        {
            Username = dto.Username,
            Password = dto.Password
        };

        User created = await dao.CreateAsync(toCreate);

        return created;
    }
    
    private static void ValidateData(UserCreationDTO dto)
    {
        string username = dto.Username;
        string password = dto.Password;

        ValidateUsername(username);
        ValidatePassword(password);
    }

    private static void ValidateUsername(string username)
    {
        if (username.Length is < 3 or > 20)
        {
            throw new UserValidationException("Username must be between 3 and 20 characters.");
        }
    }
    
    private static void ValidatePassword(string password)
    {
        if (password.Length < 6)
        {
            throw new UserValidationException("Password must be at least 6 characters.");
        }
    }
}