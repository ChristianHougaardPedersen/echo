using Domain.DTOs;
using Domain.Models;

namespace Application.ProviderInterfaces;

public interface IUserProvider
{
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO parameters);
    Task<User?> GetByUsernameAsync(string username);
}